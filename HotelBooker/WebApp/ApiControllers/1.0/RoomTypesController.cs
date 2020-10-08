using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace HotelBooker.ApiControllers._1._0
{
    /// <summary>
    /// RoomTypes
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class RoomTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RoomTypeMapper _mapper = new RoomTypeMapper();
        private readonly ReviewMapper _reviewMapper = new ReviewMapper();
        private readonly ProductMapper _productMapper = new ProductMapper();
        private readonly PriceMapper _priceMapper = new PriceMapper();
        private readonly GroupedConvenienceMapper _groupedConvenienceMapper = new GroupedConvenienceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public RoomTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of roomTypes
        /// </summary>
        /// <returns>List of roomTypes</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.RoomType>))]
        public async Task<ActionResult<IEnumerable<V1DTO.RoomType>>> GetRoomTypes(string? hotelId = null, bool forProduct = false)
        {
            if (forProduct)
            {
                var products = (await _bll.Products.GetAllAsync()).Select(o => o.RoomTypeId);
                var roomTypes = (await _bll.RoomTypes.GetAllAsync())
                    .Where(o => products.All(e => e != o.Id));
                return Ok(roomTypes);
            }
            if (!string.IsNullOrEmpty(hotelId) && hotelId != "undefined")
            {
                return Ok((await _bll.RoomTypes.GetAllAsync()).Where(o => o.HotelId == new Guid(hotelId))
                    .Select(e => _mapper.Map(e)));
            }
            return Ok((await _bll.RoomTypes.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get roomType's details
        /// </summary>
        /// <param name="id">RoomType id</param>
        /// <returns>RoomType object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RoomType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.RoomType>> GetRoomType(Guid id)
        {
            var roomType = await _bll.RoomTypes.FirstOrDefaultAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }
            
            var data = await _bll.RoomTypes.GetEarliestReservationStartDate(id);
            
            var conveniences = (await _bll.RoomTypeConveniences.GetRoomTypeConveniences(id)).ToList();
            var reviews = (await _bll.Reviews.GetRoomTypeReviews(id)).ToList();

            var result = new V1DTO.RoomTypeDetailsDTO
            {
                RoomType = _mapper.Map(roomType),
                GroupedConveniences = conveniences.Select(e => _groupedConvenienceMapper.Map(e)),
                Reviews = reviews.Select(e => _reviewMapper.Map(e)),
                AvailableRooms = data.AvailableRooms,
                EarliestReservation = data.EarliestReservation,
            };

            if (roomType.Product != null)
            {
                result.Product = _productMapper.Map(roomType.Product!);
                var bllPrices = await _bll.Prices.GetPricesForProductAsync(result.Product.Id);
                result.Prices = new List<V1DTO.Price>();
                foreach (var price in bllPrices)
                {
                    var dtoPrice = _priceMapper.Map(price);
                    dtoPrice.HotelName = price.Hotel!.Name;
                    result.Prices = result.Prices.Append(dtoPrice);
                }
            }

            return Ok(result);
        }

        /// <summary>
        /// Update a roomType
        /// </summary>
        /// <param name="id">RoomType id</param>
        /// <param name="roomType">RoomType object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutRoomType(Guid id, V1DTO.RoomType roomType)
        {
            if (id != roomType.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and RoomType.id do not match!"));
            }

            await _bll.RoomTypes.UpdateAsync(_mapper.Map(roomType));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new roomType
        /// </summary>
        /// <param name="roomType">RoomType object</param>
        /// <returns>Created roomType object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.RoomType))]
        public async Task<ActionResult<V1DTO.RoomType>> PostRoomType(V1DTO.RoomType roomType)
        {
            var bllEntity = _mapper.Map(roomType);
            _bll.RoomTypes.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = roomType.Id;
            
            return CreatedAtAction("GetRoomType", new { id = roomType.Id }, roomType);
        }

        /// <summary>
        /// Delete a roomType
        /// </summary>
        /// <param name="id">RoomType id</param>
        /// <returns>Deleted roomType object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RoomType))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.RoomType>> DeleteRoomType(Guid id)
        {
            var roomType = await _bll.RoomTypes.FirstOrDefaultAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            await _bll.RoomTypes.RemoveAsync(roomType);
            await _bll.SaveChangesAsync();

            return Ok(roomType);
        }
    }
}
