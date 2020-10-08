using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace HotelBooker.ApiControllers._1._0
{
    /// <summary>
    /// Hotels
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Microsoft.AspNetCore.Mvc.Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class HotelsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly HotelMapper _mapper = new HotelMapper();
        private readonly ReviewMapper _reviewMapper = new ReviewMapper();
        private readonly OwnerCompanyMapper _ownerCompanyMapper = new OwnerCompanyMapper();
        private readonly ConvenienceMapper _convenienceMapper = new ConvenienceMapper();
        private readonly ReviewCategoryMapper _reviewCategoryMapper = new ReviewCategoryMapper();
        private readonly GroupedConvenienceMapper _groupedConvenienceMapper = new GroupedConvenienceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public HotelsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a Hotel collection
        /// </summary>
        /// <returns>List of available Hotels</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Hotel>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Hotel>>> GetHotels()
        {
            return Ok((await _bll.Hotels.GetAllAsync()).Select(e => _mapper.Map(e)));
        }


        /// <summary>
        /// Get all possible selections for user to filter hotels by
        /// </summary>
        /// <returns>Selections object</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Hotel>))]
        public async Task<ActionResult<IEnumerable<V1DTO.FiltersSelectionsDTO>>> GetFilterSelections()
        {
            return Ok(await ConstructFilterSelections());
        }


        /// <summary>
        /// Get a filtered Hotel collection
        /// </summary>
        /// <param name="ownerCompanyId">Owner company id</param>
        /// <param name="hotelConvenienceId">Hotel convenience id</param>
        /// <param name="reviewCategoryId">Review category id</param>
        /// <param name="alphabeticalOrder">Alphabetical order value (either a-z or z-a)</param>
        /// <param name="dateEstablished">Date established order value (either Ascending or Descending)</param>
        /// <returns>Filtered Hotel collection</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Hotel>))]
        public async Task<ActionResult<IEnumerable<V1DTO.HotelIndexData>>> GetFilteredHotels(
            [FromQuery] string? ownerCompanyId, string? hotelConvenienceId, string? reviewCategoryId,
            string? alphabeticalOrder, string? dateEstablished)
        {
            var data = new V1DTO.HotelIndexData
            {
                Selections = await ConstructFilterSelections()
            };
            var bllHotels = await _bll.Hotels.GetAllAsync();
            var filters = new V1DTO.HotelFilterData
            {
                OwnerCompanyId = ownerCompanyId,
                HotelConvenienceId = hotelConvenienceId,
                ReviewCategoryId = reviewCategoryId,
                AlphabeticalOrder = alphabeticalOrder,
                DateEstablished = dateEstablished
            };
            data.Hotels = bllHotels.Select(e => _mapper.Map(e));
            if (!string.IsNullOrEmpty(filters.OwnerCompanyId) && filters.OwnerCompanyId != "undefined"
                                                              && filters.OwnerCompanyId != "null")
            {
                data.Hotels = bllHotels.Where(e => e.OwnerCompanyId == new Guid(filters.OwnerCompanyId))
                    .Select(e => _mapper.Map(e));
                data.FilterData = filters;
            }

            if (!string.IsNullOrEmpty(filters.HotelConvenienceId) && filters.HotelConvenienceId != "undefined"
                                                                  && filters.HotelConvenienceId != "null")
            {
                data.Hotels = (await _bll.Hotels
                        .GetByHotelsConvenience(new Guid(filters.HotelConvenienceId), bllHotels))
                    .Select(e => _mapper.Map(e));
            }

            if (!string.IsNullOrEmpty(filters.ReviewCategoryId) && filters.ReviewCategoryId != "undefined"
                                                                && filters.ReviewCategoryId != "null")
            {
                data.Hotels = (await _bll.Hotels
                        .GetByReviewCategory(new Guid(filters.ReviewCategoryId), bllHotels))
                    .Select(e => _mapper.Map(e));
            }

            if (!string.IsNullOrEmpty(filters.DateEstablished) && filters.DateEstablished != "undefined"
                                                               && filters.DateEstablished != "null")
            {
                if (filters.DateEstablished == "Ascending")
                {
                    data.Hotels = data.Hotels.OrderBy(e => e.Established);
                }

                if (filters.DateEstablished == "Descending")
                {
                    data.Hotels = data.Hotels.OrderByDescending(e => e.Established);
                }
            }

            if (!string.IsNullOrEmpty(filters.AlphabeticalOrder) && filters.AlphabeticalOrder != "undefined"
                                                                 && filters.AlphabeticalOrder != "null")
            {
                if (filters.AlphabeticalOrder == "a-z")
                {
                    data.Hotels = data.Hotels.OrderBy(e => e.Name);
                }

                if (filters.AlphabeticalOrder == "z-a")
                {
                    data.Hotels = data.Hotels.OrderByDescending(e => e.Name);
                }
            }

            return Ok(data);
        }

        /// <summary>
        /// Get a particular Hotel by the given id
        /// </summary>
        /// <param name="id">Hotel id</param>
        /// <returns>Requested Hotel</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.HotelDetailsDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.HotelDetailsDTO>> GetHotel(Guid id)
        {
            var hotel = await _bll.Hotels.FirstOrDefaultAsync(id);

            if (hotel == null)
            {
                return NotFound(new V1DTO.MessageDTO("Hotel not found"));
            }

            var conveniences = (await _bll.HotelConveniences.GetHotelConveniences(id)).ToList();
            var reviews = (await _bll.Reviews.GetHotelReviews(id)).ToList();

            var result = new V1DTO.HotelDetailsDTO
            {
                Hotel = _mapper.Map(hotel),
                GroupedConveniences = conveniences.Select(e => _groupedConvenienceMapper.Map(e)),
                Reviews = reviews.Select(e => _reviewMapper.Map(e))
            };

            return Ok(result);
        }


        /// <summary>
        /// Update the Hotel
        /// </summary>
        /// <param name="id">Hotel id</param>
        /// <param name="hotel">HotelDTO object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> PutHotel(Guid id, V1DTO.Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Hotel.id do not match!"));
            }

            await _bll.Hotels.UpdateAsync(_mapper.Map(hotel));
            await _bll.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Create/add a new Hotel
        /// </summary>
        /// <param name="hotel">The HotelDTO object</param>
        /// <returns>Created Hotel object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Hotel))]
        public async Task<ActionResult<V1DTO.Hotel>> PostHotel(V1DTO.Hotel hotel)
        {
            var bllEntity = _mapper.Map(hotel);
            _bll.Hotels.Add(bllEntity);
            await _bll.SaveChangesAsync();
            hotel.Id = bllEntity.Id;

            // return CreatedAtAction("GetHotel", new { id = hotel.Id, 
            //     version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" }, hotel);
            return Ok();
        }

        /// <summary>
        /// Delete the Hotel
        /// </summary>
        /// <param name="id">Hotel id</param>
        /// <returns>Deleted Hotel object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Hotel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Hotel>> DeleteHotel(Guid id)
        {
            var hotel = await _bll.Hotels.FirstOrDefaultAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            await _bll.Hotels.RemoveAsync(hotel);
            await _bll.SaveChangesAsync();

            return Ok(hotel);
        }

        private async Task<V1DTO.FiltersSelectionsDTO> ConstructFilterSelections()
        {
            return new V1DTO.FiltersSelectionsDTO
            {
                OwnerCompanySelection = (await _bll.OwnerCompanies.GetAllAsync())
                    .Select(e => _ownerCompanyMapper.Map(e)),
                ConvenienceSelection = (await _bll.HotelConveniences.GetAllAsync())
                    .Select(o => o.Convenience!)
                    .GroupBy(c => c.Id)
                    .Select(group => group.First())
                    .OrderBy(c => c.Name)
                    .Select(e => _convenienceMapper.Map(e)),
                ReviewCategorySelection = (await _bll.Reviews.GetAllCategoriesForHotelsAsync())
                    .Select(e => _reviewCategoryMapper.Map(e))
            };
        }
    }
}