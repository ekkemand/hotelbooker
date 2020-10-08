#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using BLL.App.DTO;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Convenience = BLL.App.DTO.Convenience;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class ConveniencesController : Controller
    {
        private readonly IAppBLL _bll;

        public ConveniencesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Conveniences
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Conveniences.GetAllAsync());
        }

        // GET: Conveniences/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenience = await _bll.Conveniences.FirstOrDefaultAsync(id.Value);
            if (convenience == null)
            {
                return NotFound();
            }

            return View(convenience);
        }

        // GET: Conveniences/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ConveniencesCreateEditVM
            {
                ConvenienceGroupSelectList = new SelectList(await _bll.ConvenienceGroups.GetAllAsync(),
                    nameof(ConvenienceGroup.Id), nameof(ConvenienceGroup.Name))
            };
            return View(vm);
        }

        // POST: Conveniences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConveniencesCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Conveniences.Add(vm.Convenience);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ConvenienceGroupSelectList = new SelectList(
                await _bll.OwnerCompanies.GetAllAsync(), nameof(ConvenienceGroup.Id),
                nameof(ConvenienceGroup.Name));
            return View(vm);
        }

        // GET: Conveniences/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenience = await _bll.Conveniences.FirstOrDefaultAsync(id.Value);
            if (convenience == null)
            {
                return NotFound();
            }

            var vm = new ConveniencesCreateEditVM
            {
                Convenience = convenience,
                ConvenienceGroupSelectList = new SelectList(
                    await _bll.OwnerCompanies.GetAllAsync(), nameof(ConvenienceGroup.Id),
                    nameof(ConvenienceGroup.Name)
                )
            };
            return View(vm);
        }

        // POST: Conveniences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ConveniencesCreateEditVM vm)
        {
            if (id != vm.Convenience.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Conveniences.UpdateAsync(vm.Convenience);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.ConvenienceGroupSelectList = new SelectList(
                await _bll.OwnerCompanies.GetAllAsync(), nameof(ConvenienceGroup.Id),
                nameof(ConvenienceGroup.Name)
                );
            return View(vm);
        }

        // GET: Conveniences/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenience = await _bll.Conveniences.FirstOrDefaultAsync(id.Value);
            if (convenience == null)
            {
                return NotFound();
            }

            return View(convenience);
        }

        // POST: Conveniences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Conveniences.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}