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
using RoomTypeConvenience = BLL.App.DTO.RoomTypeConvenience;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoomTypeConveniencesController : Controller
    {
        private readonly IAppBLL _bll;

        public RoomTypeConveniencesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: RoomTypeConveniences
        public async Task<IActionResult> Index()
        {
            return View(await _bll.RoomTypeConveniences.GetAllAsync());
        }

        // GET: RoomTypeConveniences/Create
        public async Task<IActionResult> Create(Guid? roomTypeId)
        {
            var roomTypes = await _bll.RoomTypes.GetAllAsync();
            if (roomTypeId != null)
            {
                roomTypes = roomTypes.Where(o => o.Id == roomTypeId);
            }
            var vm = new RoomTypeConveniencesCreateEditVM
            {
                RoomTypeSelectList = new SelectList(
                    roomTypes, nameof(RoomType.Id), nameof(RoomType.Type))
            };
            
            if (roomTypeId != null)
            {
                vm.RoomTypeConvenience = new RoomTypeConvenience
                {
                    RoomTypeId = roomTypeId.Value
                };
                var convenienceIds = (await _bll.RoomTypeConveniences.GetAllAsync())
                    .Where(o => o.RoomTypeId == roomTypeId).Select(o => o.ConvenienceId);
                
                // var conveniences = (await _bll.Conveniences.GetAllAsync())
                //     .Where(o => roomTypeConveniences.All(e => e != o.Id));
                
                var conveniences = await _bll.Conveniences.GetSuitableConveniences(convenienceIds);
                
                vm.ConvenienceSelectList = new SelectList(conveniences,
                    nameof(Convenience.Id), nameof(Convenience.Name));
            }
            return View(vm);
        }

        // POST: RoomTypeConveniences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomTypeConveniencesCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.RoomTypeConvenience.ConvenienceId == Guid.Empty)
                {
                    return RedirectToAction(nameof(Create), new {roomTypeId = vm.RoomTypeConvenience.RoomTypeId});
                }
                _bll.RoomTypeConveniences.Add(vm.RoomTypeConvenience);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new {roomTypeId = vm.RoomTypeConvenience.RoomTypeId});
            }

            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            vm.ConvenienceSelectList = new SelectList(
                await _bll.Conveniences.GetAllAsync(), nameof(Convenience.Id),
                nameof(Convenience.Name));
            return View(vm);
        }

        // GET: RoomTypeConveniences/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomTypeConvenience = await _bll.RoomTypeConveniences.FirstOrDefaultAsync(id.Value);
            if (roomTypeConvenience == null)
            {
                return NotFound();
            }
            var vm = new RoomTypeConveniencesCreateEditVM
            {
                RoomTypeConvenience = roomTypeConvenience,
                RoomTypeSelectList = new SelectList(
                    await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type)),
                ConvenienceSelectList = new SelectList(
                    await _bll.Conveniences.GetAllAsync(), nameof(Convenience.Id),
                    nameof(Convenience.Name))
            };
            return View(vm);
        }

        // POST: RoomTypeConveniences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RoomTypeConveniencesCreateEditVM vm)
        {
            if (id != vm.RoomTypeConvenience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.RoomTypeConveniences.UpdateAsync(vm.RoomTypeConvenience);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            vm.ConvenienceSelectList = new SelectList(
                await _bll.Conveniences.GetAllAsync(), nameof(Convenience.Id),
                nameof(Convenience.Name));
            return View(vm);
        }

        // GET: RoomTypeConveniences/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomTypeConvenience = await _bll.RoomTypeConveniences.FirstOrDefaultAsync(id.Value);
            if (roomTypeConvenience == null)
            {
                return NotFound();
            }

            return View(roomTypeConvenience);
        }

        // POST: RoomTypeConveniences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var roomTypeConvenience = await _bll.RoomTypeConveniences.FirstOrDefaultAsync(id);
            await _bll.RoomTypeConveniences.RemoveAsync(roomTypeConvenience);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
