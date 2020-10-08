#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ConvenienceGroup = BLL.App.DTO.ConvenienceGroup;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class ConvenienceGroupsController : Controller
    {
        private readonly IAppBLL _bll;

        public ConvenienceGroupsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ConvenienceGroups
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.ConvenienceGroups.GetAllAsync());
        }

        // GET: ConvenienceGroups/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenienceGroup = await _bll.ConvenienceGroups.FirstOrDefaultAsync(id.Value);
            if (convenienceGroup == null)
            {
                return NotFound();
            }

            return View(convenienceGroup);
        }

        // GET: ConvenienceGroups/Create
        public ViewResult Create()
        {
            return View();
        }

        // POST: ConvenienceGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConvenienceGroup convenienceGroup)
        {
            if (ModelState.IsValid)
            {
                // convenienceGroup.Id = Guid.NewGuid();
                _bll.ConvenienceGroups.Add(convenienceGroup);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(convenienceGroup);
        }

        // GET: ConvenienceGroups/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenienceGroup = await _bll.ConvenienceGroups.FirstOrDefaultAsync(id.Value);
            if (convenienceGroup == null)
            {
                return NotFound();
            }
            return View(convenienceGroup);
        }

        // POST: ConvenienceGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ConvenienceGroup convenienceGroup)
        {
            if (id != convenienceGroup.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.ConvenienceGroups.UpdateAsync(convenienceGroup);
                await _bll.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(convenienceGroup);
        }

        // GET: ConvenienceGroups/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var convenienceGroup = await _bll.ConvenienceGroups.FirstOrDefaultAsync(id.Value);
            if (convenienceGroup == null)
            {
                return NotFound();
            }

            return View(convenienceGroup);
        }

        // POST: ConvenienceGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.ConvenienceGroups.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
