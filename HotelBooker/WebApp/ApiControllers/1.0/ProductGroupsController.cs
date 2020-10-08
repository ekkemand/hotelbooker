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
    /// ProductGroups
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class ProductGroupsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductGroupMapper _mapper = new ProductGroupMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductGroupsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of productGroups
        /// </summary>
        /// <returns>List of productGroups</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ProductGroup>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ProductGroup>>> GetProductGroups()
        {
            return Ok((await _bll.ProductGroups.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get productGroup's details
        /// </summary>
        /// <param name="id">ProductGroup id</param>
        /// <returns>ProductGroup object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductGroup))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ProductGroup>> GetProductGroup(Guid id)
        {
            var productGroup = await _bll.ProductGroups.FirstOrDefaultAsync(id);

            if (productGroup == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(productGroup));
        }

        /// <summary>
        /// Update a productGroup
        /// </summary>
        /// <param name="id">ProductGroup id</param>
        /// <param name="productGroup">ProductGroup object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutProductGroup(Guid id, V1DTO.ProductGroup productGroup)
        {
            if (id != productGroup.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and ProductGroup.id do not match!"));
            }

            await _bll.ProductGroups.UpdateAsync(_mapper.Map(productGroup));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new productGroup
        /// </summary>
        /// <param name="productGroup">ProductGroup object</param>
        /// <returns>Created productGroup object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ProductGroup))]
        public async Task<ActionResult<V1DTO.ProductGroup>> PostProductGroup(V1DTO.ProductGroup productGroup)
        {
            var bllEntity = _mapper.Map(productGroup);
            _bll.ProductGroups.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = productGroup.Id;
            
            return CreatedAtAction("GetProductGroup", new { id = productGroup.Id }, productGroup);
        }

        /// <summary>
        /// Delete a productGroup
        /// </summary>
        /// <param name="id">ProductGroup id</param>
        /// <returns>Deleted productGroup object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ProductGroup))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ProductGroup>> DeleteProductGroup(Guid id)
        {
            var productGroup = await _bll.ProductGroups.FirstOrDefaultAsync(id);
            if (productGroup == null)
            {
                return NotFound();
            }

            await _bll.ProductGroups.RemoveAsync(productGroup);
            await _bll.SaveChangesAsync();

            return Ok(productGroup);
        }
    }
}
