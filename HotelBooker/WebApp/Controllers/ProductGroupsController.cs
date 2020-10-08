#pragma warning disable 1591
using System;
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
    public class ProductGroupsController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductGroupsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ProductGroups
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ProductGroups.GetAllAsync());
        }

        // GET: ProductGroups/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _bll.ProductGroups.FirstOrDefaultAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // GET: ProductGroups/Create
        public async Task<IActionResult> Create()
        {
            var vm = new ProductGroupsCreateEditVM
            {
                ParentGroupSelectList = new SelectList(
                    await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name))
            };
            return View(vm);
        }

        // POST: ProductGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductGroupsCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _bll.ProductGroups.Add(vm.ProductGroup);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ParentGroupSelectList = new SelectList(
                await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name));
            return View(vm);
        }

        // GET: ProductGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _bll.ProductGroups.FirstOrDefaultAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }

            var vm = new ProductGroupsCreateEditVM
            {
                ProductGroup = productGroup,
                ParentGroupSelectList = new SelectList(
                    await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name))
            };
            return View(vm);
        }

        // POST: ProductGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductGroupsCreateEditVM vm)
        {
            if (id != vm.ProductGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ProductGroups.UpdateAsync(vm.ProductGroup);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            vm.ParentGroupSelectList = new SelectList(
                await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name));
            return View(vm);
        }

        // GET: ProductGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productGroup = await _bll.ProductGroups.FirstOrDefaultAsync(id.Value);
            if (productGroup == null)
            {
                return NotFound();
            }

            return View(productGroup);
        }

        // POST: ProductGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productGroup = await _bll.ProductGroups.FirstOrDefaultAsync(id);
            await _bll.ProductGroups.RemoveAsync(productGroup);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}