#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoomTypesController : Controller
    {
        private readonly IAppBLL _bll;

        public RoomTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: RoomTypes
        [AllowAnonymous]
        public async Task<IActionResult> Index(Guid? hotelId)
        {
            if (hotelId != null)
            {
                return View((await _bll.RoomTypes.GetAllAsync()).Where(o => o.HotelId == hotelId));
            }
            return View(await _bll.RoomTypes.GetAllAsync());
        }

        // GET: RoomTypes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _bll.RoomTypes.FirstOrDefaultAsync(id.Value);
            if (roomType == null)
            {
                return NotFound();
            }
            var data = await _bll.RoomTypes.GetEarliestReservationStartDate(id.Value);
            data.RoomType.Reviews = (await _bll.Reviews.GetRoomTypeReviews(roomType.Id)).ToList();

            if (data.RoomType.Product != null)
            {
                data.RoomType.Product.Prices = await _bll.Prices.GetPricesForProductAsync(data.RoomType.Product.Id);
            }

            var conveniences = (await _bll.RoomTypeConveniences.GetRoomTypeConveniences(id.Value)).ToList();
            
            var vm = new RoomTypesDetailsVM
            {
                RoomType = data.RoomType,
                AvailableRooms = data.AvailableRooms,
                EarliestReservation = data.EarliestReservation,
                GroupedConveniences = conveniences
            };

            return View(vm);
        }

        // GET: RoomTypes/Create
        public async Task<ViewResult> Create(Guid? hotelId)
        {
            var hotels = await _bll.Hotels.GetAllAsync();
            if (hotelId != null)
            {
                hotels = hotels.Where(o => o.Id == hotelId);
            }
            var vm = new RoomTypesCreateEditVM
            {
                HotelSelectList = new SelectList(
                    hotels, nameof(Hotel.Id), nameof(Hotel.Name))
            };
            return View(vm);
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomTypesCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _bll.RoomTypes.Add(vm.RoomType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            return View(vm);
        }

        // GET: RoomTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _bll.RoomTypes.FirstOrDefaultAsync(id.Value);
            if (roomType == null)
            {
                return NotFound();
            }
            var vm = new RoomTypesCreateEditVM
            {
                RoomType = roomType,
                HotelSelectList = new SelectList(
                    await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name))
            };
            return View(vm);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RoomTypesCreateEditVM vm)
        {
            if (id != vm.RoomType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.RoomTypes.UpdateAsync(vm.RoomType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            return View(vm);
        }

        // GET: RoomTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomType = await _bll.RoomTypes.FirstOrDefaultAsync(id.Value);
            if (roomType == null)
            {
                return NotFound();
            }

            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.RoomTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
