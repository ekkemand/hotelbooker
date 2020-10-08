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
    /// RoomTypeConveniences
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class RoomTypeConveniencesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RoomTypeConvenienceMapper _mapper = new RoomTypeConvenienceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public RoomTypeConveniencesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of roomTypeConveniences
        /// </summary>
        /// <returns>List of roomTypeConveniences</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.RoomTypeConvenience>))]
        public async Task<ActionResult<IEnumerable<V1DTO.RoomTypeConvenience>>> GetRoomTypeConveniences()
        {
            var bllConveniences = await _bll.RoomTypeConveniences.GetAllAsync();
            var dtoConveniences = new List<V1DTO.RoomTypeConvenience>();
            foreach (var convenience in bllConveniences)
            {
                var dtoElement = _mapper.Map(convenience);
                dtoElement.RoomTypeName = convenience.RoomType!.Type;
                dtoElement.ConvenienceName = convenience.Convenience!.Name;
                dtoConveniences.Add(dtoElement);
            }
            return Ok(dtoConveniences);
        }

        /// <summary>
        /// Get roomTypeConvenience's details
        /// </summary>
        /// <param name="id">RoomTypeConvenience id</param>
        /// <returns>RoomTypeConvenience object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RoomTypeConvenience))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.RoomTypeConvenience>> GetRoomTypeConvenience(Guid id)
        {
            var roomTypeConvenience = await _bll.RoomTypeConveniences.FirstOrDefaultAsync(id);

            if (roomTypeConvenience == null)
            {
                return NotFound();
            }
            
            var dtoElement = _mapper.Map(roomTypeConvenience);
            dtoElement.ConvenienceName = roomTypeConvenience.Convenience!.Name;
            dtoElement.RoomTypeName = roomTypeConvenience.RoomType!.Type;

            return Ok(dtoElement);
        }

        /// <summary>
        /// Update a roomTypeConvenience
        /// </summary>
        /// <param name="id">RoomTypeConvenience id</param>
        /// <param name="roomTypeConvenience">RoomTypeConvenience object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutRoomTypeConvenience(Guid id, V1DTO.RoomTypeConvenience roomTypeConvenience)
        {
            if (id != roomTypeConvenience.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and RoomTypeConvenience.id do not match!"));
            }

            await _bll.RoomTypeConveniences.UpdateAsync(_mapper.Map(roomTypeConvenience));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new roomTypeConvenience
        /// </summary>
        /// <param name="roomTypeConvenience">RoomTypeConvenience object</param>
        /// <returns>Created roomTypeConvenience object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.RoomTypeConvenience))]
        public async Task<ActionResult<V1DTO.RoomTypeConvenience>> PostRoomTypeConvenience(V1DTO.RoomTypeConvenience roomTypeConvenience)
        {
            var bllEntity = _mapper.Map(roomTypeConvenience);
            _bll.RoomTypeConveniences.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = roomTypeConvenience.Id;
            
            return CreatedAtAction("GetRoomTypeConvenience", new { id = roomTypeConvenience.Id }, roomTypeConvenience);
        }

        /// <summary>
        /// Delete a roomTypeConvenience
        /// </summary>
        /// <param name="id">RoomTypeConvenience id</param>
        /// <returns>Deleted roomTypeConvenience object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.RoomTypeConvenience))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.RoomTypeConvenience>> DeleteRoomTypeConvenience(Guid id)
        {
            var roomTypeConvenience = await _bll.RoomTypeConveniences.FirstOrDefaultAsync(id);
            if (roomTypeConvenience == null)
            {
                return NotFound();
            }

            await _bll.RoomTypeConveniences.RemoveAsync(roomTypeConvenience);
            await _bll.SaveChangesAsync();

            return Ok(roomTypeConvenience);
        }
    }
}
