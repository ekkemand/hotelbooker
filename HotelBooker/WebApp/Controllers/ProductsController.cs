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

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Products.GetAllAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Products.FirstOrDefaultAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var products = (await _bll.Products.GetAllAsync()).Select(o => o.RoomTypeId);
            var roomTypes = (await _bll.RoomTypes.GetAllAsync())
                .Where(o => products.All(e => e != o.Id));
            var vm = new ProductsCreateEditVM
            {
                RoomTypeSelectList = new SelectList(roomTypes, nameof(RoomType.Id), nameof(RoomType.Type)),
                ProductGroupSelectList = new SelectList(
                    await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name))
            };
            return View(vm);
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductsCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Products.Add(vm.Product);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            vm.ProductGroupSelectList = new SelectList(
                await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name));
            return View(vm);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Products.FirstOrDefaultAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            
            var vm = new ProductsCreateEditVM
            {
                Product = product,
                RoomTypeSelectList = new SelectList(
                    await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type)),
                ProductGroupSelectList = new SelectList(
                    await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name))
            };
            return View(vm);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductsCreateEditVM vm)
        {
            if (id != vm.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Products.UpdateAsync(vm.Product);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.RoomTypeSelectList = new SelectList(
                await _bll.RoomTypes.GetAllAsync(), nameof(RoomType.Id), nameof(RoomType.Type));
            vm.ProductGroupSelectList = new SelectList(
                await _bll.ProductGroups.GetAllAsync(), nameof(ProductGroup.Id), nameof(ProductGroup.Name));
            return View(vm);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _bll.Products.FirstOrDefaultAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);
            await _bll.Products.RemoveAsync(product);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
