#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using Contracts.BLL.App;
using Extensions;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class ReservationsController : Controller
    {
        private readonly IAppBLL _bll;

        public ReservationsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("admin"))
            {
                return View(await _bll.Reservations.GetAllAsync());
            }

            return View(await _bll.Reservations.GetAllAsync(User.UserId()));
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _bll.Reservations.FirstOrDefaultAsync(id.Value, User.UserId());
            if (reservation == null)
            {
                return NotFound();
            }
            
            var vm = new ReservationDetailsVM
            {
                Reservation = reservation,
                ReservationRows = await _bll.ReservationRows.GetReservationRowsByReservationId(id.Value)
            };

            vm.TakenProducts = vm.ReservationRows.Select(o => o.Product!);

            vm.Prices = await _bll.Prices.GetPricesForProducts(await _bll.Products.GetOtherProducts(vm.TakenProducts));
            
            return View(vm);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create(int roomsCount, DateTime minDate, Guid hotelId, Guid roomTypeId)
        {
            var hotels = (await _bll.Hotels.GetAllAsync()).Where(o => o.Id == hotelId);
            var roomTypes = (await _bll.RoomTypes.GetAllAsync()).Where(o => o.Id == roomTypeId);
            var vm = new ReservationCreateEditVM
            {
                MaxRooms = roomsCount,
                MinDate = minDate,
                HotelSelectList = new SelectList(
                    hotels, nameof(Hotel.Id), nameof(Hotel.Name), hotelId),
                RoomCountSelectList = new SelectList(Enumerable.Range(1, roomsCount)),
                RoomTypeSelectList = new SelectList(
                    roomTypes, nameof(RoomType.Id), nameof(RoomType.Type), roomTypeId)
            };
            return View(vm);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationCreateEditVM vm)
        {
            var data = await _bll.RoomTypes.GetEarliestReservationStartDate(vm.Reservation.RoomTypeId);
            if (ModelState.IsValid)
            {
                if (DateTime.Compare(vm.Reservation.StartDateTime, data.EarliestReservation!.Value) >= 0)
                {
                    vm.Reservation.PersonId = (await _bll.Users.FirstOrDefaultAsync(User.UserId())).PersonId;
                    vm.Reservation.UserId = User.UserId();
                    await _bll.ReservationRows.AddRowWithReservation(vm.Reservation);
                    await _bll.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                vm.Error = $"Reservation start date is set too early, should be {data.EarliestReservation} or later!";
            }

            var hotels = (await _bll.Hotels.GetAllAsync())
                .Where(o => o.Id == vm.Reservation.HotelId);
            var roomTypes = (await _bll.RoomTypes.GetAllAsync())
                .Where(o => o.Id == vm.Reservation.RoomTypeId);
            vm.HotelSelectList = new SelectList(
                hotels, nameof(Hotel.Id), nameof(Hotel.Name));
            vm.RoomCountSelectList = new SelectList(Enumerable.Range(1, data.AvailableRooms));
            vm.RoomTypeSelectList = new SelectList(
                roomTypes, nameof(RoomType.Id), nameof(RoomType.Type));
            return View(vm);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _bll.Reservations.FirstOrDefaultAsync(id.Value, User.UserId());
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Reservations.RemoveAsync(id, User.UserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Person> GetPerson()
        {
            var user = GetUser().Result;

            return await _bll.Persons.FirstOrDefaultAsync(user.PersonId!);
        }

        private async Task<AppUser> GetUser()
        {
            return await _bll.Users.FirstOrDefaultAsync(User.UserId());
        }
    }
}