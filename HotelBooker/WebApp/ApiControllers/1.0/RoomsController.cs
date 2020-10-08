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
    /// Rooms
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class RoomsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly RoomMapper _mapper = new RoomMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public RoomsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of rooms
        /// </summary>
        /// <returns>List of rooms</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Room>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Room>>> GetRooms()
        {
            var rooms = await _bll.Rooms.GetAllAsync();
            var roomDtos = new List<V1DTO.Room>();
            foreach (var room in rooms)
            {
                var dtoRoom = _mapper.Map(room);
                dtoRoom.HotelName = room.Hotel!.Name;
                dtoRoom.RoomTypeName = room.RoomType!.Type;
                roomDtos.Add(dtoRoom);
            }
            return Ok(roomDtos);
        }

        /// <summary>
        /// Get room's details
        /// </summary>
        /// <param name="id">Room id</param>
        /// <returns>Room object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Room))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Room>> GetRoom(Guid id)
        {
            var room = await _bll.Rooms.FirstOrDefaultAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            var dtoRoom = _mapper.Map(room);
            dtoRoom.HotelName = room.Hotel!.Name;
            dtoRoom.RoomTypeName = room.RoomType!.Type;

            return Ok(dtoRoom);
        }

        /// <summary>
        /// Update a room
        /// </summary>
        /// <param name="id">Room id</param>
        /// <param name="room">Room object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutRoom(Guid id, V1DTO.Room room)
        {
            if (id != room.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Room.id do not match!"));
            }

            await _bll.Rooms.UpdateAsync(_mapper.Map(room));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new room
        /// </summary>
        /// <param name="room">Room object</param>
        /// <returns>Created room object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Room))]
        public async Task<ActionResult<V1DTO.Room>> PostRoom(V1DTO.Room room)
        {
            var bllEntity = _mapper.Map(room);
            _bll.Rooms.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = room.Id;
            
            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        /// <summary>
        /// Delete a room
        /// </summary>
        /// <param name="id">Room id</param>
        /// <returns>Deleted room object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Room))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Room>> DeleteRoom(Guid id)
        {
            var room = await _bll.Rooms.FirstOrDefaultAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            await _bll.Rooms.RemoveAsync(room);
            await _bll.SaveChangesAsync();

            return Ok(room);
        }
    }
}
