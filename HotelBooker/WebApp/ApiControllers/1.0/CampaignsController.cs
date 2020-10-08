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
    /// Campaigns
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class CampaignsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CampaignMapper _mapper = new CampaignMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CampaignsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of campaigns
        /// </summary>
        /// <returns>List of campaigns</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Campaign>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Campaign>>> GetCampaigns()
        {
            return Ok((await _bll.Campaigns.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get campaign's details
        /// </summary>
        /// <param name="id">Campaign id</param>
        /// <returns>Campaign object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Campaign))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Campaign>> GetCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id);

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(campaign));
        }

        /// <summary>
        /// Update a campaign
        /// </summary>
        /// <param name="id">Campaign id</param>
        /// <param name="campaign">Campaign object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutCampaign(Guid id, V1DTO.Campaign campaign)
        {
            if (id != campaign.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Campaign.id do not match!"));
            }

            await _bll.Campaigns.UpdateAsync(_mapper.Map(campaign));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new campaign
        /// </summary>
        /// <param name="campaign">Campaign object</param>
        /// <returns>Created campaign object</returns>
        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Campaign))]
        public async Task<ActionResult<V1DTO.Campaign>> PostCampaign(V1DTO.Campaign campaign)
        {
            var bllEntity = _mapper.Map(campaign);
            _bll.Campaigns.Add(bllEntity);
            await _bll.SaveChangesAsync();
            campaign.Id = bllEntity.Id;

            return CreatedAtAction(nameof(GetCampaign), new {id = campaign.Id}, campaign);
        }

        /// <summary>
        /// Delete a campaign
        /// </summary>
        /// <param name="id">Campaign id</param>
        /// <returns>Deleted campaign object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Campaign))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Campaign>> DeleteCampaign(Guid id)
        {
            var campaign = await _bll.Campaigns.FirstOrDefaultAsync(id);
            if (campaign == null)
            {
                return NotFound();
            }

            await _bll.Campaigns.RemoveAsync(campaign);
            await _bll.SaveChangesAsync();

            return Ok(campaign);
        }
    }
}
