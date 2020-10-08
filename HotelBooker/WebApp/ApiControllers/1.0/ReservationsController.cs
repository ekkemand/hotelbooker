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
    /// Reservations
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "user")]
    public class ReservationsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly ReservationMapper _mapper = new ReservationMapper();
        private readonly ReservationRowMapper _reservationRowMapper = new ReservationRowMapper();
        private readonly ProductMapper _productMapper = new ProductMapper();
        private readonly PriceMapper _priceMapper = new PriceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public ReservationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        /// <summary>
        /// Get a list of reservations
        /// </summary>
        /// <returns>List of reservations</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Reservation>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Reservation>>> GetReservations(string? userId)
        {
            if (!string.IsNullOrEmpty(userId) && userId != "undefined")
            {
                return Ok((await _bll.Reservations.GetAllAsync()).Where(o => o.UserId == new Guid(userId))
                    .Select(e => _mapper.Map(e)));
            }
            return Ok((await _bll.Reservations.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get reservation's details
        /// </summary>
        /// <param name="id">Reservation id</param>
        /// <returns>Reservation object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Reservation))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.ReservationDetailsDTO>> GetReservation(Guid id)
        {
            var reservation = await _bll.Reservations.FirstOrDefaultAsync(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var reservationRows = await _bll.ReservationRows.GetReservationRowsByReservationId(id);
            
            var reservationDetails = new V1DTO.ReservationDetailsDTO()
            {
                Reservation = _mapper.Map(reservation),
                ReservationRows = reservationRows.Select(e => _reservationRowMapper.Map(e))
            };

            reservationDetails.TakenProducts = reservationRows.Select(o => o.Product!).Select(e => _productMapper.Map(e));

            reservationDetails.Prices = 
                (await _bll.Prices.GetPricesForProducts(await _bll.Products.GetOtherProducts(reservationRows
                    .Select(o => o.Product!)))).Select(e => _priceMapper.Map(e));

            return Ok(reservationDetails);
        }

        /// <summary>
        /// Update a reservation
        /// </summary>
        /// <param name="id">Reservation id</param>
        /// <param name="reservation">Reservation object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(V1DTO.MessageDTO))]
        public async Task<IActionResult> PutReservation(Guid id, V1DTO.Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest(new V1DTO.MessageDTO("id and Reservation.id do not match!"));
            }

            await _bll.Reservations.UpdateAsync(_mapper.Map(reservation));
            await _bll.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Add a new reservation
        /// </summary>
        /// <param name="reservation">Reservation object</param>
        /// <returns>Created reservation object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Reservation))]
        public async Task<ActionResult<V1DTO.Reservation>> PostReservation(V1DTO.Reservation reservation)
        {
            var bllEntity = _mapper.Map(reservation);
            var user = await _bll.Users.FirstOrDefaultAsync(reservation.UserId);
            bllEntity.PersonId = user.PersonId;
            
            await _bll.ReservationRows.AddRowWithReservation(bllEntity);
            await _bll.SaveChangesAsync();
            bllEntity.Id = reservation.Id;
            
            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        /// <summary>
        /// Delete a reservation
        /// </summary>
        /// <param name="id">Reservation id</param>
        /// <returns>Deleted reservation object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Reservation))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(V1DTO.MessageDTO))]
        public async Task<ActionResult<V1DTO.Reservation>> DeleteReservation(Guid id)
        {
            var reservation = await _bll.Reservations.FirstOrDefaultAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            await _bll.Reservations.RemoveAsync(reservation);
            await _bll.SaveChangesAsync();

            return Ok(reservation);
        }
    }
}
