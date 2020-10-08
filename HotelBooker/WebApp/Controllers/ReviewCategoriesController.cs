#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authorization;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReviewCategoriesController : Controller
    {
        private readonly IAppBLL _bll;

        public ReviewCategoriesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ReviewCategories
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ReviewCategories.GetAllAsync());
        }

        // GET: ReviewCategories/Create
        public ViewResult Create()
        {
            return View();
        }

        // POST: ReviewCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewCategory reviewCategory)
        {
            if (ModelState.IsValid)
            {
                _bll.ReviewCategories.Add(reviewCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(reviewCategory);
        }

        // GET: ReviewCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewCategory = await _bll.ReviewCategories.FirstOrDefaultAsync(id.Value);
            if (reviewCategory == null)
            {
                return NotFound();
            }

            return View(reviewCategory);
        }

        // POST: ReviewCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ReviewCategory reviewCategory)
        {
            if (id != reviewCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ReviewCategories.UpdateAsync(reviewCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(reviewCategory);
        }

        // GET: ReviewCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewCategory = await _bll.ReviewCategories.FirstOrDefaultAsync(id.Value);
            if (reviewCategory == null)
            {
                return NotFound();
            }

            return View(reviewCategory);
        }

        // POST: ReviewCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ReviewCategories.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Accept(Guid id)
        {
            await _bll.ReviewCategories.AcceptCategory(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}