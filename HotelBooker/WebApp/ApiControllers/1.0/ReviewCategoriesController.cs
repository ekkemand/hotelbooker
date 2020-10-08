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
    /// ReviewCategories
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    
    public class ReviewCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ReviewCategoryMapper _mapper = new ReviewCategoryMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ReviewCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of reviewCategories
        /// </summary>
        /// <returns>List of reviewCategories</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ReviewCategory>))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        public async Task<ActionResult<IEnumerable<V1DTO.ReviewCategory>>> GetReviewCategories()
        {
            return Ok((await _bll.ReviewCategories.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get reviewCategory's details
        /// </summary>
        /// <param name="id">ReviewCategory id</param>
        /// <returns>ReviewCategory object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ReviewCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<V1DTO.ReviewCategory>> GetReviewCategory(Guid id)
        {
            var reviewCategory = await _bll.ReviewCategories.FirstOrDefaultAsync(id);

            if (reviewCategory == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(reviewCategory));
        }

        /// <summary>
        /// Update a reviewCategory
        /// </summary>
        /// <param name="id">ReviewCategory id</param>
        /// <param name="reviewCategory">ReviewCategory object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> PutReviewCategory(Guid id, V1DTO.ReviewCategory reviewCategory)
        {
            if (id != reviewCategory.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and ReviewCategory.id do not match!"));
            }

            await _bll.ReviewCategories.UpdateAsync(_mapper.Map(reviewCategory));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new reviewCategory
        /// </summary>
        /// <param name="reviewCategory">ReviewCategory object</param>
        /// <returns>Created reviewCategory object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ReviewCategory))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<V1DTO.ReviewCategory>> PostReviewCategory(V1DTO.ReviewCategory reviewCategory)
        {
            var bllEntity = _mapper.Map(reviewCategory);
            _bll.ReviewCategories.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = reviewCategory.Id;
            
            return CreatedAtAction("GetReviewCategory", new { id = reviewCategory.Id }, reviewCategory);
        }

        /// <summary>
        /// Delete a reviewCategory
        /// </summary>
        /// <param name="id">ReviewCategory id</param>
        /// <returns>Deleted reviewCategory object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ReviewCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<V1DTO.ReviewCategory>> DeleteReviewCategory(Guid id)
        {
            var reviewCategory = await _bll.ReviewCategories.FirstOrDefaultAsync(id);
            if (reviewCategory == null)
            {
                return NotFound(new {message = "Category not found"});
            }

            await _bll.ReviewCategories.RemoveAsync(reviewCategory);
            await _bll.SaveChangesAsync();

            return Ok(reviewCategory);
        }
    }
}
