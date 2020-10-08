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
    /// Convenience groups
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class ConvenienceGroupsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ConvenienceGroupMapper _mapper = new ConvenienceGroupMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ConvenienceGroupsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of convenience groups
        /// </summary>
        /// <returns>List of convenience groups</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ConvenienceGroup>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ConvenienceGroup>>> GetConvenienceGroups()
        {
            return Ok((await _bll.ConvenienceGroups.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get convenience group's details
        /// </summary>
        /// <param name="id">Convenience group id</param>
        /// <returns>Convenience group object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ConvenienceGroup))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ConvenienceGroup>> GetConvenienceGroup(Guid id)
        {
            var convenienceGroup = await _bll.ConvenienceGroups.FirstOrDefaultAsync(id);

            if (convenienceGroup == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(convenienceGroup));
        }

        /// <summary>
        /// Update a convenience group
        /// </summary>
        /// <param name="id">Convenience group id</param>
        /// <param name="convenienceGroup">Convenience group object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutConvenienceGroup(Guid id, V1DTO.ConvenienceGroup convenienceGroup)
        {
            if (id != convenienceGroup.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and ConvenienceGroup.id do not match!"));
            }
            
            await _bll.ConvenienceGroups.UpdateAsync(_mapper.Map(convenienceGroup));
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Add a new convenience group
        /// </summary>
        /// <param name="convenienceGroup">Convenience group object</param>
        /// <returns>Created convenience group object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ConvenienceGroup))]
        public async Task<ActionResult<V1DTO.ConvenienceGroup>> PostConvenienceGroup(V1DTO.ConvenienceGroup convenienceGroup)
        {
            var bllEntity = _mapper.Map(convenienceGroup);
            _bll.ConvenienceGroups.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = convenienceGroup.Id;

            return CreatedAtAction("GetConvenienceGroup", new { id = convenienceGroup.Id }, convenienceGroup);
        }

        // DELETE: api/ConvenienceGroups/5
        /// <summary>
        /// Delete a convenience group
        /// </summary>
        /// <param name="id">Convenience group id</param>
        /// <returns>Deleted convenience group object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ConvenienceGroup))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ConvenienceGroup>> DeleteConvenienceGroup(Guid id)
        {
            var convenienceGroups = await _bll.ConvenienceGroups.FirstOrDefaultAsync(id);
            if (convenienceGroups == null)
            {
                return NotFound();
            }

            await _bll.ConvenienceGroups.RemoveAsync(convenienceGroups);
            await _bll.SaveChangesAsync();

            return Ok(convenienceGroups);
        }
    }
}
