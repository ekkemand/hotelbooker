#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class ReservationRowsController : Controller
    {
        private readonly IAppBLL _bll;

        public ReservationRowsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ReservationRows
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("admin"))
            {
                return View(await _bll.ReservationRows.GetAllAsync());
            }
            return View(await _bll.ReservationRows.GetAllAsync(User.UserId()));
        }

        // GET: ReservationRows/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ReservationRowsCreateEditVM
            {
                RoomTypeSelectList = new SelectList(
                    await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type)),
                ReservationSelectList = new SelectList(
                    await _bll.Reservations.GetAllAsync(), nameof(Reservation.Id),
                    nameof(Reservation.Hotel.Name))
            };
            return View(vm);
        }

        // POST: ReservationRows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid productId, Guid reservationId)
        {
            var row = new ReservationRow
            {
                ProductId = productId,
                ReservationId = reservationId
            };
            
            _bll.ReservationRows.Add(row);
            await _bll.SaveChangesAsync();
            Console.WriteLine($"Reservation: {reservationId}");
            Console.WriteLine($"Product: {productId}");
            return RedirectToAction("Details", "Reservations", new {id = reservationId});
        }


        // GET: ReservationRows/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationRow = await _bll.ReservationRows.FirstOrDefaultAsync(id.Value);
            if (reservationRow == null)
            {
                return NotFound();
            }

            return View(reservationRow);
        }

        // POST: ReservationRows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reservationRow = await _bll.ReservationRows.FirstOrDefaultAsync(id);
            var reservationId = reservationRow.ReservationId;
            await _bll.ReservationRows.RemoveAsync(reservationRow);
            await _bll.SaveChangesAsync();
            return RedirectToAction("Details", "Reservations", new {id = reservationId});
        }
    }
}
