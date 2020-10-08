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
    /// HotelConveniences
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class HotelConveniencesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly HotelConvenienceMapper _mapper = new HotelConvenienceMapper();

        /// <summary>
        /// Constructor 
        /// </summary>
        public HotelConveniencesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of hotelConveniences
        /// </summary>
        /// <returns>List of hotelConveniences</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.HotelConvenience>))]
        public async Task<ActionResult<IEnumerable<V1DTO.HotelConvenience>>> GetHotelConveniences()
        {
            return Ok((await _bll.HotelConveniences.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get hotelConvenience's details
        /// </summary>
        /// <param name="id">HotelConvenience id</param>
        /// <returns>HotelConvenience object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.HotelConvenience))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.HotelConvenience>> GetHotelConvenience(Guid id)
        {
            var hotelConvenience = await _bll.HotelConveniences.FirstOrDefaultAsync(id);

            if (hotelConvenience == null)
            {
                return NotFound();
            }

            return _mapper.Map(hotelConvenience);
        }

        /// <summary>
        /// Update a hotelConvenience
        /// </summary>
        /// <param name="id">HotelConvenience id</param>
        /// <param name="hotelConvenience">HotelConvenience object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutHotelConvenience(Guid id, V1DTO.HotelConvenience hotelConvenience)
        {
            if (id != hotelConvenience.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and HotelConvenience.id do not match!"));
            }

            await _bll.HotelConveniences.UpdateAsync(_mapper.Map(hotelConvenience));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Add a new hotelConvenience
        /// </summary>
        /// <param name="hotelConvenience">HotelConvenience object</param>
        /// <returns>HotelConvenience hotelConvenience object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.HotelConvenience))]
        public async Task<ActionResult<V1DTO.HotelConvenience>> PostHotelConvenience(V1DTO.HotelConvenience hotelConvenience)
        {
            var bllEntity = _mapper.Map(hotelConvenience);
            _bll.HotelConveniences.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = hotelConvenience.Id;

            return CreatedAtAction("GetHotelConvenience", new {id = hotelConvenience.Id}, hotelConvenience);
        }

        /// <summary>
        /// Delete a campaign
        /// </summary>
        /// <param name="id">Campaign id</param>
        /// <returns>Deleted campaign object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Campaign))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.HotelConvenience>> DeleteHotelConvenience(Guid id)
        {
            var hotelConvenience = await _bll.HotelConveniences.FirstOrDefaultAsync(id);
            if (hotelConvenience == null)
            {
                return NotFound();
            }

            await _bll.HotelConveniences.RemoveAsync(hotelConvenience);
            await _bll.SaveChangesAsync();

            return Ok(hotelConvenience);
        }
    }
}