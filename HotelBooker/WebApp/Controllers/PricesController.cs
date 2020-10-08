#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using HotelBooker.Helpers;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Price = BLL.App.DTO.Price;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class PricesController : Controller
    {
        private readonly IAppBLL _bll;

        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Prices.GetAllAsync());
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public async Task<IActionResult> Create()
        {
            var helper = new SelectListCombiner();
            
            var vm = new PricesCreateEditVM
            {
                CurrencySelectList = new SelectList(
                    await _bll.Currencies.GetAllAsync(), nameof(Currency.Id), nameof(Currency.Name)),
                CampaignSelectList = helper.NullableCampaignList(await _bll.Campaigns.GetAllAsync()),
                HotelSelectList = new SelectList(
                    await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name)),
                ProductSelectList = new SelectList(
                    await _bll.Products.GetAllAsync(), nameof(Product.Id), nameof(Product.Name))
            };
            return View(vm);
        }

        // POST: Prices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PricesCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(vm.NewCurrency))
                {
                    vm.Price.Currency = new Currency
                    {
                        Name = vm.NewCurrency
                    };
                }

                if (vm.Price.CampaignId == Guid.Empty)
                {
                    vm.Price.CampaignId = null;
                }
                
                _bll.Prices.Add(vm.Price);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var helper = new SelectListCombiner();
            vm.CurrencySelectList = new SelectList(
                await _bll.Currencies.GetAllAsync(), nameof(Currency.Id), nameof(Currency.Name));
            vm.CampaignSelectList = helper.NullableCampaignList(await _bll.Campaigns.GetAllAsync());
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            vm.ProductSelectList = new SelectList(
                await _bll.Products.GetAllAsync(), nameof(Product.Id), nameof(Product.Name));
            return View(vm);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }
            var helper = new SelectListCombiner();
            var vm = new PricesCreateEditVM
            {
                Price = price,
                CurrencySelectList = new SelectList(
                    await _bll.Currencies.GetAllAsync(), nameof(Currency.Id), nameof(Currency.Name)),
                CampaignSelectList = helper.NullableCampaignList(await _bll.Campaigns.GetAllAsync()),
                HotelSelectList = new SelectList(
                    await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name)),
                ProductSelectList = new SelectList(
                    await _bll.Products.GetAllAsync(), nameof(Product.Id), nameof(Product.Name)),
            };
            return View(vm);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PricesCreateEditVM vm)
        {
            if (id != vm.Price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(vm.NewCurrency))
                {
                    vm.Price.Currency = new Currency
                    {
                        Name = vm.NewCurrency
                    };
                }
                await _bll.Prices.UpdateAsync(vm.Price);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            var helper = new SelectListCombiner();

            vm.CurrencySelectList = new SelectList(
                await _bll.Currencies.GetAllAsync(), nameof(Currency.Id), nameof(Currency.Name));
            vm.CampaignSelectList = new SelectList(helper.NullableCampaignList(await _bll.Campaigns.GetAllAsync()),
                nameof(Campaign.Id), nameof(Campaign.Name));
            vm.HotelSelectList = new SelectList(
                await _bll.Hotels.GetAllAsync(), nameof(Hotel.Id), nameof(Hotel.Name));
            vm.ProductSelectList = new SelectList(
                await _bll.Products.GetAllAsync(), nameof(Product.Id), nameof(Product.Name));
            return View(vm);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            await _bll.Prices.RemoveAsync(price);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
