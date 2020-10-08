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
using Convenience = DAL.App.DTO.Convenience;
using Hotel = DAL.App.DTO.Hotel;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class HotelConveniencesController : Controller
    {
        private readonly IAppBLL _bll;

        public HotelConveniencesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: HotelConveniences
        public async Task<IActionResult> Index()
        {
            return View(await _bll.HotelConveniences.GetAllAsync());
        }

        // GET: HotelConveniences/Create
        public async Task<IActionResult> Create(Guid? hotelId)
        {
            var hotels = await _bll.Hotels.GetAllAsync();
            if (hotelId != null)
            {
                hotels = hotels.Where(o => o.Id == hotelId);
            }
            var vm = new HotelConveniencesCreateEditVM
            {
                HotelSelectList = new SelectList(
                    hotels, nameof(Hotel.Id), nameof(Hotel.Name))
            };
            
            if (hotelId != null)
            {
                vm.HotelConvenience = new HotelConvenience
                {
                    HotelId = hotelId.Value
                };
                var convenienceIds = (await _bll.HotelConveniences.GetAllAsync())
                    .Where(o => o.HotelId == hotelId).Select(o => o.ConvenienceId);
                
                // var conveniences = (await _bll.Conveniences.GetAllAsync())
                //     .Where(o => hotelConveniences.All(e => e != o.Id));

                var conveniences = await _bll.Conveniences.GetSuitableConveniences(convenienceIds);
                
                vm.ConvenienceSelectList = new SelectList(conveniences,
                    nameof(Convenience.Id), nameof(Convenience.Name));
            }
            return View(vm);
        }

        // POST: HotelConveniences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelConveniencesCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HotelConvenience.ConvenienceId == Guid.Empty)
                {
                    return RedirectToAction(nameof(Create), new {hotelId = vm.HotelConvenience.HotelId});
                }
                _bll.HotelConveniences.Add(vm.HotelConvenience);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Create), new {hotelId = vm.HotelConvenience.HotelId});
            }
            vm.ConvenienceSelectList = new SelectList(
                await _bll.Conveniences.GetAllAsync(), nameof(Convenience.Id), nameof(Convenience.Name));
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            return View(vm);
        }

        // GET: HotelConveniences/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelConvenience = await _bll.HotelConveniences.FirstOrDefaultAsync(id.Value);
            if (hotelConvenience == null)
            {
                return NotFound();
            }
            var vm = new HotelConveniencesCreateEditVM
            {
                HotelConvenience = hotelConvenience,
                ConvenienceSelectList = new SelectList(
                    await _bll.Conveniences.GetAllAsync(), nameof(Convenience.Id), nameof(Convenience.Name)),
                HotelSelectList = new SelectList(
                    await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name))
            };
            return View(vm);
        }

        // POST: HotelConveniences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, HotelConveniencesCreateEditVM vm)
        {
            if (id != vm.HotelConvenience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.HotelConveniences.UpdateAsync(vm.HotelConvenience);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ConvenienceSelectList = new SelectList(
                await _bll.Conveniences.GetAllAsync(), nameof(Convenience.Id), nameof(Convenience.Name));
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            return View(vm);
        }

        // GET: HotelConveniences/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotelConvenience = await _bll.HotelConveniences.FirstOrDefaultAsync(id.Value);
            if (hotelConvenience == null)
            {
                return NotFound();
            }

            return View(hotelConvenience);
        }

        // POST: HotelConveniences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.HotelConveniences.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
