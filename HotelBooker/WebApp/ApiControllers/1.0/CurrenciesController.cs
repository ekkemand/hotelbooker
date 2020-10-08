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
    /// Currencies
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class CurrenciesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CurrencyMapper _mapper = new CurrencyMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CurrenciesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of currencies
        /// </summary>
        /// <returns>List of currencies</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Currency>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Currency>>> GetCurrencies()
        {
            return Ok((await _bll.Currencies.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get currency's details
        /// </summary>
        /// <param name="id">Currency id</param>
        /// <returns>Currency object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Currency))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Currency>> GetCurrency(Guid id)
        {
            var currency = await _bll.Currencies.FirstOrDefaultAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return _mapper.Map(currency);
        }

        /// <summary>
        /// Update a currency
        /// </summary>
        /// <param name="id">Currency id</param>
        /// <param name="currency">Currency object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCurrency(Guid id, V1DTO.Currency currency)
        {
            if (id != currency.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Currency.id do not match!"));
            }

            await _bll.Currencies.UpdateAsync(_mapper.Map(currency));
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        /// <summary>
        /// Add a new currency
        /// </summary>
        /// <param name="currency">Currency object</param>
        /// <returns>Currency currency object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Currency))]
        public async Task<ActionResult<V1DTO.Currency>> PostCurrency(V1DTO.Currency currency)
        {
            var bllEntity = _mapper.Map(currency);
            _bll.Currencies.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = currency.Id;

            return CreatedAtAction("GetCurrency", new {id = currency.Id}, currency);
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
        public async Task<ActionResult<V1DTO.Currency>> DeleteCurrency(Guid id)
        {
            var currency = await _bll.Currencies.FirstOrDefaultAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            await _bll.Currencies.RemoveAsync(currency);
            await _bll.SaveChangesAsync();

            return Ok(currency);
        }
    }
}