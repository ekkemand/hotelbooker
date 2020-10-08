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
    /// ImageOfRooms
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ImageOfRoomsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ImageOfRoomMapper _mapper = new ImageOfRoomMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ImageOfRoomsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of imageOfRooms
        /// </summary>
        /// <returns>List of imageOfRooms</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ImageOfRoom>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ImageOfRoom>>> GetImageOfRooms([FromQuery] string? roomTypeId)
        {
            IEnumerable<BLL.App.DTO.ImageOfRoom> bllImages;
            if (!string.IsNullOrEmpty(roomTypeId) && roomTypeId != "undefined")
            {
                bllImages = (await _bll.ImageOfRooms.GetAllAsync())
                    .Where(o => o.RoomTypeId == new Guid(roomTypeId));
            }
            else
            {
                bllImages = await _bll.ImageOfRooms.GetAllAsync();
            }
            
            var dtoImages = new List<V1DTO.ImageOfRoom>();
            foreach (var image in bllImages)
            {
                var dtoImage = _mapper.Map(image);
                dtoImage.HotelName = image.Hotel!.Name;
                dtoImage.RoomTypeName = image.RoomType!.Type;
                dtoImages.Add(dtoImage);
            }
            return Ok(dtoImages);
        }

        /// <summary>
        /// Get imageOfRoom's details
        /// </summary>
        /// <param name="id">ImageOfRoom id</param>
        /// <returns>ImageOfRoom object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ImageOfRoom))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ImageOfRoom>> GetImageOfRoom(Guid id)
        {
            var imageOfRoom = await _bll.ImageOfRooms.FirstOrDefaultAsync(id);

            if (imageOfRoom == null)
            {
                return NotFound();
            }

            var dtoImage = _mapper.Map(imageOfRoom);
            dtoImage.HotelName = imageOfRoom.Hotel!.Name;
            dtoImage.RoomTypeName = imageOfRoom.RoomType!.Type;

            return Ok(dtoImage);
        }

        /// <summary>
        /// Update a imageOfRoom
        /// </summary>
        /// <param name="id">ImageOfRoom id</param>
        /// <param name="imageOfRoom">ImageOfRoom object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutImageOfRoom(Guid id, V1DTO.ImageOfRoom imageOfRoom)
        {
            if (id != imageOfRoom.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and ImageOfRoom.id do not match!"));
            }

            await _bll.ImageOfRooms.UpdateAsync(_mapper.Map(imageOfRoom));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new imageOfRoom
        /// </summary>
        /// <param name="imageOfRoom">ImageOfRoom object</param>
        /// <returns>Created imageOfRoom object</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ImageOfRoom))]
        public async Task<ActionResult<V1DTO.ImageOfRoom>> PostImageOfRoom(V1DTO.ImageOfRoom imageOfRoom)
        {
            var bllEntity = _mapper.Map(imageOfRoom);
            _bll.ImageOfRooms.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = imageOfRoom.Id;
            
            return CreatedAtAction("GetImageOfRoom", new { id = imageOfRoom.Id }, imageOfRoom);
        }

        /// <summary>
        /// Delete a imageOfRoom
        /// </summary>
        /// <param name="id">ImageOfRoom id</param>
        /// <returns>Deleted imageOfRoom object</returns>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ImageOfRoom))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ImageOfRoom>> DeleteImageOfRoom(Guid id)
        {
            var imageOfRoom = await _bll.ImageOfRooms.FirstOrDefaultAsync(id);
            if (imageOfRoom == null)
            {
                return NotFound();
            }

            await _bll.ImageOfRooms.RemoveAsync(imageOfRoom);
            await _bll.SaveChangesAsync();

            return Ok(imageOfRoom);
        }
    }
}
