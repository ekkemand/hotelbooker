using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
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
    /// Prices
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class PricesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PriceMapper _mapper = new PriceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of prices
        /// </summary>
        /// <returns>List of prices</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Price>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Price>>> GetPrices()
        {
            return Ok((await _bll.Prices.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get price's details
        /// </summary>
        /// <param name="id">Price id</param>
        /// <returns>Price object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Price))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Price>> GetPrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);

            if (price == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(price));
        }

        /// <summary>
        /// Update a price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <param name="price">Price object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutPrice(Guid id, V1DTO.Price price)
        {
            if (id != price.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Price.id do not match!"));
            }

            price.Campaign = null;
            price.Currency = null;
            price.Product = null;
            var bllEntity = _mapper.Map(price);
            
            if (!string.IsNullOrEmpty(price.NewCurrency))
            {
                bllEntity.Currency = new Currency
                {
                    Name = price.NewCurrency
                };
            }

            await _bll.Prices.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new price
        /// </summary>
        /// <param name="price">Price object</param>
        /// <returns>Created price object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Price))]
        public async Task<ActionResult<V1DTO.Price>> PostPrice(V1DTO.Price price)
        {
            var bllEntity = _mapper.Map(price);

            if (!string.IsNullOrEmpty(price.NewCurrency))
            {
                bllEntity.Currency = new Currency
                {
                    Name = price.NewCurrency
                };
            }
            
            _bll.Prices.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = price.Id;
            
            return CreatedAtAction("GetPrice", new { id = price.Id }, price);
        }

        /// <summary>
        /// Delete a price
        /// </summary>
        /// <param name="id">Price id</param>
        /// <returns>Deleted price object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Price))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Price>> DeletePrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            price.Campaign = null;
            price.Currency = null;
            price.Product = null;

            await _bll.Prices.RemoveAsync(price);
            await _bll.SaveChangesAsync();

            return Ok(price);
        }
    }
}
