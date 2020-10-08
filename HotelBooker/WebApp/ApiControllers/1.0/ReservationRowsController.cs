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
    /// ReservationRows
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    public class ReservationRowsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ReservationRowMapper _mapper = new ReservationRowMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ReservationRowsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of reservationRows
        /// </summary>
        /// <returns>List of reservationRows</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.ReservationRow>))]
        public async Task<ActionResult<IEnumerable<V1DTO.ReservationRow>>> GetReservationRows()
        {
            return Ok((await _bll.ReservationRows.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get reservationRow's details
        /// </summary>
        /// <param name="id">ReservationRow id</param>
        /// <returns>ReservationRow object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ReservationRow))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ReservationRow>> GetReservationRow(Guid id)
        {
            var reservationRow = await _bll.ReservationRows.FirstOrDefaultAsync(id);

            if (reservationRow == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map(reservationRow));
        }

        /// <summary>
        /// Update a reservationRow
        /// </summary>
        /// <param name="id">ReservationRow id</param>
        /// <param name="reservationRow">ReservationRow object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutReservationRow(Guid id, V1DTO.ReservationRow reservationRow)
        {
            if (id != reservationRow.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and ReservationRow.id do not match!"));
            }

            await _bll.ReservationRows.UpdateAsync(_mapper.Map(reservationRow));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new reservationRow
        /// </summary>
        /// <param name="reservationRow">ReservationRow object</param>
        /// <returns>Created reservationRow object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.ReservationRow))]
        public async Task<ActionResult<V1DTO.ReservationRow>> PostReservationRow(V1DTO.ReservationRow reservationRow)
        {
            var bllEntity = _mapper.Map(reservationRow);
            _bll.ReservationRows.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = reservationRow.Id;
            
            return CreatedAtAction("GetReservationRow", new { id = reservationRow.Id }, reservationRow);
        }

        /// <summary>
        /// Delete a reservationRow
        /// </summary>
        /// <param name="id">ReservationRow id</param>
        /// <returns>Deleted reservationRow object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.ReservationRow))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ReservationRow>> DeleteReservationRow(Guid id)
        {
            var reservationRow = await _bll.ReservationRows.FirstOrDefaultAsync(id);
            if (reservationRow == null)
            {
                return NotFound();
            }

            await _bll.ReservationRows.RemoveAsync(reservationRow);
            await _bll.SaveChangesAsync();

            return Ok(reservationRow);
        }
    }
}
