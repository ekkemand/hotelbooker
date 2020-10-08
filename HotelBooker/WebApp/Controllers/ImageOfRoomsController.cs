#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ImageOfRoom = BLL.App.DTO.ImageOfRoom;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class ImageOfRoomsController : Controller
    {
        private readonly IAppBLL _bll;

        public ImageOfRoomsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ImageOfRooms
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ImageOfRooms.GetAllAsync());
        }

        // GET: ImageOfRooms/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageOfRoom = await _bll.ImageOfRooms.FirstOrDefaultAsync(id.Value);
            if (imageOfRoom == null)
            {
                return NotFound();
            }

            return View(imageOfRoom);
        }

        // GET: ImageOfRooms/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ImageOfRoomsCreateEditVM
            {
                HotelSelectList = new SelectList(
                    await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name)),
                RoomTypeSelectList = new SelectList(
                    await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type))
            };
            return View(vm);
        }

        // POST: ImageOfRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ImageOfRoomsCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _bll.ImageOfRooms.Add(vm.ImageOfRoom);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            return View(vm);
        }

        // GET: ImageOfRooms/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageOfRoom = await _bll.ImageOfRooms.FirstOrDefaultAsync(id.Value);
            if (imageOfRoom == null)
            {
                return NotFound();
            }
            var vm = new ImageOfRoomsCreateEditVM
            {
                ImageOfRoom = imageOfRoom,
                HotelSelectList = new SelectList(
                    await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name)),
                RoomTypeSelectList = new SelectList(
                    await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type))
            };
            return View(vm);
        }

        // POST: ImageOfRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ImageOfRoomsCreateEditVM vm)
        {
            if (id != vm.ImageOfRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ImageOfRooms.UpdateAsync(vm.ImageOfRoom);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            return View(vm);
        }

        // GET: ImageOfRooms/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageOfRoom = await _bll.ImageOfRooms.FirstOrDefaultAsync(id.Value);
            if (imageOfRoom == null)
            {
                return NotFound();
            }

            return View(imageOfRoom);
        }

        // POST: ImageOfRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ImageOfRooms.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
