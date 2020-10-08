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
    /// Products
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ProductMapper _mapper = new ProductMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of products
        /// </summary>
        /// <returns>List of products</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Product>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Product>>> GetProducts()
        {
            return Ok((await _bll.Products.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get product's details
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Product>> GetProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var dtoProduct = _mapper.Map(product);
            dtoProduct.ProductGroupName = product.ProductGroup!.Name;
            if (product.RoomTypeId != null)
            {
                dtoProduct.RoomTypeName = product.RoomType!.Type;
            }

            return Ok(dtoProduct);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">Product object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutProduct(Guid id, V1DTO.Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Product.id do not match!"));
            }

            await _bll.Products.UpdateAsync(_mapper.Map(product));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        /// <param name="product">Product object</param>
        /// <returns>Created product object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Product))]
        public async Task<ActionResult<V1DTO.Product>> PostProduct(V1DTO.Product product)
        {
            var bllEntity = _mapper.Map(product);
            _bll.Products.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = product.Id;
            
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Deleted product object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Product>> DeleteProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await _bll.Products.RemoveAsync(product);
            await _bll.SaveChangesAsync();

            return Ok(product);
        }
    }
}
