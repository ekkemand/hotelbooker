using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO = PublicApi.DTO.v1;

namespace HotelBooker.ApiControllers._1._0
{
    /// <summary>
    /// Reviews
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    public class ReviewsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ReviewMapper _mapper = new ReviewMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ReviewsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of reviews
        /// </summary>
        /// <returns>List of reviews</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Review>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Review>>> GetReviews(string? hotelId, string? roomTypeId, string? userId)
        {
            var reviews = !string.IsNullOrEmpty(userId) && userId != "undefined"
                ? await _bll.Reviews.GetAllAsync(new Guid(userId))
                : await _bll.Reviews.GetAllAsync();

            if (!string.IsNullOrEmpty(hotelId) && hotelId != "undefined")
            {
                reviews = reviews.Where(o => o.HotelId == new Guid(hotelId));
            }
            if (!string.IsNullOrEmpty(roomTypeId) && roomTypeId != "undefined")
            {
                reviews = reviews.Where(o => o.RoomTypeId == new Guid(roomTypeId));
            }
            
            var reviewsForApi = new List<V1DTO.Review>();
            foreach (var review in reviews)
            {
                var dtoReview = _mapper.Map(review);
                dtoReview.UserDisplayName = review.User!.DisplayName;
                dtoReview.HotelName = review.Hotel!.Name;
                reviewsForApi.Add(dtoReview);
            }
            return Ok(reviewsForApi);
        }

        /// <summary>
        /// Get review's details
        /// </summary>
        /// <param name="id">Review id</param>
        /// <returns>Review object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Review))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Review>> GetReview(Guid id)
        {
            var review = await _bll.Reviews.FirstOrDefaultAsync(id);

            if (review == null)
            {
                return NotFound();
            }

            var dtoReview = _mapper.Map(review);
            dtoReview.UserDisplayName = review.User!.DisplayName;
            dtoReview.HotelName = review.Hotel!.Name;
            if (review.RoomTypeId != null)
            {
                dtoReview.RoomTypeName = review.RoomType!.Type;
            }

            return Ok(dtoReview);
        }

        /// <summary>
        /// Update a review
        /// </summary>
        /// <param name="id">Review id</param>
        /// <param name="review">Review object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutReview(Guid id, V1DTO.Review review)
        {
            if (id != review.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Review.id do not match!"));
            }

            review.ReviewCategory = null;

            var bllEntity = _mapper.Map(review);
            
            if (!string.IsNullOrEmpty(review.NewCategoryString))
            {
                bllEntity.ReviewCategory = new ReviewCategory
                {
                    Name = review.NewCategoryString
                };
            }

            await _bll.Reviews.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new review
        /// </summary>
        /// <param name="review">Review object</param>
        /// <returns>Created review object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Review))]
        public async Task<ActionResult<V1DTO.Review>> PostReview(V1DTO.Review review)
        {
            var bllEntity = _mapper.Map(review);
            if (!string.IsNullOrEmpty(review.NewCategoryString))
            {
                bllEntity.ReviewCategory = new ReviewCategory
                {
                    Name = review.NewCategoryString
                };
            }
            _bll.Reviews.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = review.Id;
            
            return CreatedAtAction("GetReview", new { id = review.Id }, review);
        }

        /// <summary>
        /// Delete a review
        /// </summary>
        /// <param name="id">Review id</param>
        /// <returns>Deleted review object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Review))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Review>> DeleteReview(Guid id)
        {
            var review = await _bll.Reviews.FirstOrDefaultAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            await _bll.Reviews.RemoveAsync(review);
            await _bll.SaveChangesAsync();

            return Ok(review);
        }
    }
}
