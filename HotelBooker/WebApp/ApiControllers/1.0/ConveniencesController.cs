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
    /// Conveniences
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class ConveniencesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ConvenienceMapper _mapper = new ConvenienceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ConveniencesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of conveniences
        /// </summary>
        /// <returns>List of conveniences</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Convenience>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Convenience>>> GetConveniences(string? hotelId, string? roomTypeId)
        {
            if (!string.IsNullOrEmpty(hotelId) && hotelId != "undefined")
            {
                var hotelConveniences = (await _bll.HotelConveniences.GetAllAsync())
                    .Where(o => o.HotelId == new Guid(hotelId));
                var suitableConveniences =  await _bll.Conveniences
                    .GetSuitableConveniences(hotelConveniences.Select(o => o.ConvenienceId));
                
                return Ok(suitableConveniences.Select(e => _mapper.Map(e)));
            }
            if (!string.IsNullOrEmpty(roomTypeId) && roomTypeId != "undefined")
            {
                var roomTypeConveniences = (await _bll.RoomTypeConveniences.GetAllAsync())
                    .Where(o => o.RoomTypeId == new Guid(roomTypeId));
                var suitableConveniences =  await _bll.Conveniences
                    .GetSuitableConveniences(roomTypeConveniences.Select(o => o.ConvenienceId));
                
                return Ok(suitableConveniences.Select(e => _mapper.Map(e)));
            }
            return Ok((await _bll.Conveniences.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get convenience's details
        /// </summary>
        /// <param name="id">Convenience id</param>
        /// <returns>Convenience object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Convenience))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Convenience>> GetConvenience(Guid id)
        {
            var convenience = await _bll.Conveniences.FirstOrDefaultAsync(id);

            if (convenience == null)
            {
                return NotFound();
            }
            
            var dtoConvenience = _mapper.Map(convenience);
            dtoConvenience.ConvenienceGroupName = convenience.ConvenienceGroup!.Name;

            return dtoConvenience;
        }

        /// <summary>
        /// Update a convenience
        /// </summary>
        /// <param name="id">Convenience id</param>
        /// <param name="convenience">Convenience object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutConvenience(Guid id, V1DTO.Convenience convenience)
        {
            if (id != convenience.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Convenience.id do not match!"));
            }

            await _bll.Conveniences.UpdateAsync(_mapper.Map(convenience));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add a new convenience
        /// </summary>
        /// <param name="convenience">Convenience object</param>
        /// <returns>Convenience convenience object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Convenience))]
        public async Task<ActionResult<V1DTO.Convenience>> PostConvenience(V1DTO.Convenience convenience)
        {
            var bllEntity = _mapper.Map(convenience);
            _bll.Conveniences.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = convenience.Id;

            return CreatedAtAction("GetConvenience", new { id = convenience.Id }, convenience);
        }

        /// <summary>
        /// Delete a convenience
        /// </summary>
        /// <param name="id">Convenience id</param>
        /// <returns>Deleted convenience object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Convenience))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Convenience>> DeleteConvenience(Guid id)
        {
            var convenience = await _bll.Conveniences.FirstOrDefaultAsync(id);
            if (convenience == null)
            {
                return NotFound();
            }

            await _bll.Conveniences.RemoveAsync(convenience);
            await _bll.SaveChangesAsync();

            return Ok(convenience);
        }
    }
}
