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
    /// OwnerCompanies
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class OwnerCompaniesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly OwnerCompanyMapper _mapper = new OwnerCompanyMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public OwnerCompaniesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of ownerCompanies
        /// </summary>
        /// <returns>List of ownerCompanies</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.OwnerCompany>))]
        public async Task<ActionResult<IEnumerable<V1DTO.OwnerCompany>>> GetOwnerCompanies()
        {
            return Ok((await _bll.OwnerCompanies.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get ownerCompany's details
        /// </summary>
        /// <param name="id">OwnerCompany id</param>
        /// <returns>OwnerCompany object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OwnerCompany))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.OwnerCompany>> GetOwnerCompany(Guid id)
        {
            var ownerCompany = await _bll.OwnerCompanies.FirstOrDefaultAsync(id);

            if (ownerCompany == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(ownerCompany));
        }

        /// <summary>
        /// Update a ownerCompany
        /// </summary>
        /// <param name="id">OwnerCompany id</param>
        /// <param name="ownerCompany">OwnerCompany object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutOwnerCompany(Guid id, V1DTO.OwnerCompany ownerCompany)
        {
            if (id != ownerCompany.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and OwnerCompany.id do not match!"));
            }

            await _bll.OwnerCompanies.UpdateAsync(_mapper.Map(ownerCompany));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new ownerCompany
        /// </summary>
        /// <param name="ownerCompany">OwnerCompany object</param>
        /// <returns>Created ownerCompany object</returns>
        [HttpPost]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        // [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.OwnerCompany))]
        public async Task<ActionResult<V1DTO.OwnerCompany>> PostOwnerCompany(V1DTO.OwnerCompany ownerCompany)
        {
            var bllEntity = _mapper.Map(ownerCompany);
            _bll.OwnerCompanies.Add(bllEntity);
            await _bll.SaveChangesAsync();
            ownerCompany.Id = bllEntity.Id;
            
            return CreatedAtAction("GetOwnerCompany", new { id = ownerCompany.Id }, ownerCompany);
        }

        /// <summary>
        /// Delete a ownerCompany
        /// </summary>
        /// <param name="id">OwnerCompany id</param>
        /// <returns>Deleted ownerCompany object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.OwnerCompany))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.OwnerCompany>> DeleteOwnerCompany(Guid id)
        {
            var ownerCompany = await _bll.OwnerCompanies.FirstOrDefaultAsync(id);
            if (ownerCompany == null)
            {
                return NotFound();
            }

            await _bll.OwnerCompanies.RemoveAsync(ownerCompany);
            await _bll.SaveChangesAsync();

            return Ok(ownerCompany);
        }
    }
}
