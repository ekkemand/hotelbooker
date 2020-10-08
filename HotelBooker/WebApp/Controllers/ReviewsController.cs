#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin,user")]
    public class ReviewsController : Controller
    {
        private readonly IAppBLL _bll;

        public ReviewsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Reviews
        
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("admin"))
            {
                return View(await _bll.Reviews.GetAllAsync());
            }
            return View(await _bll.Reviews.GetAllAsync(User.UserId()));
        }

        // GET: Reviews/Details/5
        
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _bll.Reviews.FirstOrDefaultAsync(id.Value);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        
        public async Task<IActionResult> Create(Guid hotelId, Guid? roomTypeId)
        {
            
            var vm = new ReviewsCreateEditVM
            {
                Review = new Review
                {
                    HotelId = hotelId,
                    RoomTypeId = roomTypeId
                },
                ReviewCategorySelectList = new SelectList(
                    (await _bll.ReviewCategories.GetAllAsync()).Where(c => c.Accepted),
                    nameof(ReviewCategory.Id), nameof(ReviewCategory.Name), null)
            };
            Console.WriteLine(vm.Review.HotelId);
            return View(vm);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewsCreateEditVM vm)
        {
            if (ModelState.IsValid)
            {
                vm.Review.UserId = User.UserId();
                if (vm.CustomCategoryName != null && !vm.CustomCategoryName.Equals(""))
                {
                    vm.Review.ReviewCategory = new ReviewCategory
                    {
                        Name = vm.CustomCategoryName
                    };
                }
                _bll.Reviews.Add(vm.Review);
                await _bll.SaveChangesAsync();
                if (vm.Review.RoomTypeId == null)
                {
                    return RedirectToAction(nameof(Details), "Hotels", new {id = vm.Review.HotelId});
                }
                return RedirectToAction(nameof(Details), "RoomTypes", new {id = vm.Review.RoomTypeId});
            }
            vm.ReviewCategorySelectList = new SelectList(
                await _bll.ReviewCategories.GetAllAsync(), nameof(ReviewCategory.Id),
                nameof(ReviewCategory.Name));
            return View(vm);
        }

        // GET: Reviews/Edit/5
        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review review;
            if (User.IsInRole("admin"))
            {
                review = await _bll.Reviews.FirstOrDefaultAsync(id.Value);
            }
            else
            {
                review = await _bll.Reviews.FirstOrDefaultAsync(id.Value, User.UserId());
            }
            if (review == null)
            {
                return NotFound();
            }
            if (!User.IsInRole("admin"))
            {
                if (User.UserId() != review.UserId)
                {
                    return Redirect(Url.Content("~/"));
                }
            }

            var vm = new ReviewsCreateEditVM
            {
                Review = review,
                ReviewCategorySelectList = new SelectList(
                    (await _bll.ReviewCategories.GetAllAsync()).Where(c => c.Accepted),
                    nameof(ReviewCategory.Id), nameof(ReviewCategory.Name), null)
            };
            return View(vm);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(Guid id, ReviewsCreateEditVM vm)
        {
            if (id != vm.Review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                {
                    await _bll.Reviews.UpdateAsync(vm.Review);
                    await _bll.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(vm);
        }

        // GET: Reviews/Delete/5
        
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _bll.Reviews.FirstOrDefaultAsync(id.Value);
            
            if (review == null)
            {
                return NotFound();
            }
            if (!User.IsInRole("admin"))
            {
                if (User.UserId() != review.UserId)
                {
                    return Redirect(Url.Content("~/"));
                }
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var review = await _bll.Reviews.FirstOrDefaultAsync(id);
            await _bll.Reviews.RemoveAsync(review);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
