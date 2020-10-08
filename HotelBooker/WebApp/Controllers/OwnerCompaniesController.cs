#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwnerCompany = BLL.App.DTO.OwnerCompany;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class OwnerCompaniesController : Controller
    {
        private readonly IAppBLL _bll;

        public OwnerCompaniesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: OwnerCompanies
        public async Task<IActionResult> Index()
        {
            return View(await _bll.OwnerCompanies.GetAllAsync());
        }

        // GET: OwnerCompanies/Create
        public ViewResult Create()
        {
            return View();
        }

        // POST: OwnerCompanies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerCompany ownerCompany)
        {
            if (ModelState.IsValid)
            {
                _bll.OwnerCompanies.Add(ownerCompany);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ownerCompany);
        }

        // GET: OwnerCompanies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerCompany = await _bll.OwnerCompanies.FirstOrDefaultAsync(id.Value);
            if (ownerCompany == null)
            {
                return NotFound();
            }
            return View(ownerCompany);
        }

        // POST: OwnerCompanies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, OwnerCompany ownerCompany)
        {
            if (id != ownerCompany.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.OwnerCompanies.UpdateAsync(ownerCompany);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ownerCompany);
        }

        // GET: OwnerCompanies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ownerCompany = await _bll.OwnerCompanies.FirstOrDefaultAsync(id.Value);
            if (ownerCompany == null)
            {
                return NotFound();
            }

            return View(ownerCompany);
        }

        // POST: OwnerCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.OwnerCompanies.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
