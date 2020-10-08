#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Currency = BLL.App.DTO.Currency;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class CurrenciesController : Controller
    {
        private readonly IAppBLL _bll;

        public CurrenciesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Currences
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Currencies.GetAllAsync());
        }

        // GET: Currences/Create
        public ViewResult Create()
        {
            return View();
        }

        // POST: Currences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Currency currency)
        {
            if (ModelState.IsValid)
            {
                _bll.Currencies.Add(currency);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currences/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _bll.Currencies.FirstOrDefaultAsync(id.Value);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Currencies.UpdateAsync(currency);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currences/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _bll.Currencies.FirstOrDefaultAsync(id.Value);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Currencies.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
