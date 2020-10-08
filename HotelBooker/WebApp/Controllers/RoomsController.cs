#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Room = BLL.App.DTO.Room;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoomsController : Controller
    {
        private readonly IAppBLL _bll;

        public RoomsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(Guid? hotelId)
        {
            if (hotelId != null)
            {
                return View((await _bll.Rooms.GetAllAsync()).Where(r => r.HotelId == hotelId));
            }
            return View(await _bll.Rooms.GetAllAsync());
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _bll.Rooms.FirstOrDefaultAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        public async Task<IActionResult> Create(Guid? hotelId)
        {
            var hotels = await _bll.Hotels.GetAllAsync();
            if (hotelId != null)
            {
                hotels = hotels.Where(o => o.Id == hotelId);
            }
            var vm = new RoomsCreateEditVM
            {
                RoomTypeSelectList = new SelectList(
                    await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type)),
                HotelSelectList = new SelectList(
                    hotels, nameof(Hotel.Id), nameof(Hotel.Name))
            };
            return View(vm);
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomsCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Rooms.Add(vm.Room);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {hotelId = vm.Room.HotelId});
            }
            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            return View(vm);
        }

        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _bll.Rooms.FirstOrDefaultAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }
            var vm = new RoomsCreateEditVM
            {
                Room = room,
                RoomTypeSelectList = new SelectList(
                    await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type)),
                HotelSelectList = new SelectList(
                    await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id),
                    nameof(Hotel.Name))
            };
            return View(vm);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RoomsCreateEditVM vm)
        {
            if (id != vm.Room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Rooms.UpdateAsync(vm.Room);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            return View(vm);
        }

        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _bll.Rooms.FirstOrDefaultAsync(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var room = await _bll.Rooms.FirstOrDefaultAsync(id);
            await _bll.Rooms.RemoveAsync(room);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
