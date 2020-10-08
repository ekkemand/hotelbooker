#pragma warning disable 1591
using System;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;
using Contracts.BLL.App;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;
        

        public UsersController(IAppBLL bll, UserManager<Domain.App.Identity.AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Users.GetAllAsync());
        }
        
        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = (await _bll.Users.FirstOrDefaultAsync(id.Value)).Person;
            var user = await _userManager.FindByIdAsync(id.Value.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var vm = new UsersDetailsVM
            {
                User = user,
                Person = person!,
                RolesList = _userManager.GetRolesAsync(user).Result
            };

            return View(vm);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = (await _bll.Users.FirstOrDefaultAsync(id.Value)).Person;
            var user = await _userManager.FindByIdAsync(id.Value.ToString());
            if (user == null)
            {
                return NotFound();
            }
            var vm = new UsersDetailsVM
            {
                User = user,
                Person = person!,
                RolesList = _userManager.GetRolesAsync(user).Result
            };

            return View(vm);
        }

        // POST: Users/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Users.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MakeAdmin(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.AddToRoleAsync(user, "admin");
            return RedirectToAction(nameof(Details), new {id = userId});
        }
        
        public async Task<IActionResult> RemoveAdmin(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.RemoveFromRoleAsync(user, "admin");
            return RedirectToAction(nameof(Details), new {id = userId});
        }
    }
}
