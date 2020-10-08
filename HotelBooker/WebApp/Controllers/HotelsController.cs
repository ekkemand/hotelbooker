#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.DTO;
using Extensions;
using HotelBooker.Helpers;
using HotelBooker.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.OpenApi.Extensions;
using Hotel = BLL.App.DTO.Hotel;

namespace HotelBooker.Controllers
{
    [Authorize(Roles = "admin")]
    public class HotelsController : Controller
    {
        private readonly IAppBLL _bll;

        public HotelsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Hotels
        [AllowAnonymous]
        public async Task<IActionResult> Index(HotelFilterHelperVM vm)
        {
            var generalOptionsForView = new GeneralOrderingOptions().OptionsForView;
            var helper = new SelectListCombiner();
            var conveniences = (await _bll.HotelConveniences.GetAllAsync())
                .Select(o => o.Convenience!)
                .GroupBy(c => c.Id)
                .Select(group => group.First())
                .OrderBy(c => c.Name);

            if (vm.FilterData == null)
            {
                vm = new HotelFilterHelperVM
                {
                    Hotels = await _bll.Hotels.GetAllAsync(),
                    OwnerCompanySelectList = helper.NullableOwnersSelectList(await _bll.OwnerCompanies.GetAllAsync()),
                    ConvenienceSelectList = helper.NullableConveniencesSelectList(conveniences),
                    ReviewCategorySelectList = helper.NullableReviewCategoriesSelectList(
                        await _bll.Reviews.GetAllCategoriesForHotelsAsync()),
                    AlphabeticalOrderOptions = new SelectList(generalOptionsForView),
                    EstablishedDateOrderOptions = new SelectList(generalOptionsForView)
                };
                vm.FilterData = new HotelFilterData();
                return View(vm);
            }
            
            if (vm.Hotels == null)
            {
                vm.Hotels = await _bll.Hotels.GetAllAsync();
            }
            var generalOptions = new GeneralOrderingOptions().Options;
            
            if (vm.FilterData.HotelConvenienceId != null && vm.FilterData.HotelConvenienceId != Guid.Empty)
            {
                vm.Hotels = await _bll.Hotels.GetByHotelsConvenience(vm.FilterData.HotelConvenienceId.Value, vm.Hotels);
            }
            if (vm.FilterData.ReviewCategoryId != null && vm.FilterData.ReviewCategoryId != Guid.Empty)
            {
                vm.Hotels = await _bll.Hotels.GetByReviewCategory(vm.FilterData.ReviewCategoryId!.Value, vm.Hotels);
            }
            if (vm.FilterData.OwnerCompanyId != null && vm.FilterData.OwnerCompanyId != Guid.Empty)
            {
                vm.Hotels = vm.Hotels.Where(o => o.OwnerCompanyId == vm.FilterData.OwnerCompanyId);
                Console.WriteLine(vm.Hotels.Count());
            }

            if (vm.FilterData.AlphabeticalOrder != null && vm.FilterData.AlphabeticalOrder != generalOptions[OrderOption.Default])
            {
                if (vm.FilterData.AlphabeticalOrder == generalOptions[OrderOption.Ascending])
                {
                    vm.Hotels = vm.Hotels.OrderBy(o => o.Name);
                }
                if (vm.FilterData.AlphabeticalOrder == generalOptions[OrderOption.Descending])
                {
                    vm.Hotels = vm.Hotels.OrderByDescending(o => o.Name);
                }
            }
            if (vm.FilterData.DateEstablished != null && vm.FilterData.DateEstablished != generalOptions[OrderOption.Default])
            {
                if (vm.FilterData.DateEstablished == generalOptions[OrderOption.Ascending])
                {
                    vm.Hotels = vm.Hotels.OrderBy(o => o.Established);
                }
                if (vm.FilterData.DateEstablished == generalOptions[OrderOption.Descending])
                {
                    vm.Hotels = vm.Hotels.OrderByDescending(o => o.Established);
                }
            }

            vm.OwnerCompanySelectList = helper.NullableOwnersSelectList(await _bll.OwnerCompanies.GetAllAsync());
            vm.ConvenienceSelectList = helper.NullableConveniencesSelectList(conveniences);
            vm.ReviewCategorySelectList = helper.NullableReviewCategoriesSelectList(
                await _bll.Reviews.GetAllCategoriesForHotelsAsync());
            vm.AlphabeticalOrderOptions = new SelectList(generalOptionsForView);
            vm.EstablishedDateOrderOptions = new SelectList(generalOptionsForView);

            return View(vm);
        }

        // GET: Hotels/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _bll.Hotels.FirstOrDefaultAsync(id.Value);
            if (hotel == null)
            {
                return NotFound();
            }

            var conveniences = (await _bll.HotelConveniences.GetHotelConveniences(id.Value)).ToList();
            var reviews = (await _bll.Reviews.GetHotelReviews(id.Value)).ToList();

            var vm = new HotelEntityVM
            {
                Hotel = hotel,
                GroupedConveniences = conveniences,
                Reviews = reviews
            };
            
            return View(vm);
        }

        // GET: Hotels/Create
        public async Task<IActionResult> Create()
        {
            var vm = new HotelEntityVM
            {
                OwnerCompanySelectList = new SelectList(
                    await _bll.OwnerCompanies.GetAllAsync(), nameof(OwnerCompany.Id), nameof(OwnerCompany.Name))
            };
            return View(vm);
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HotelEntityVM vm)
        {
            if (ModelState.IsValid)
            {
                _bll.Hotels.Add(vm.Hotel);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            vm.OwnerCompanySelectList = new SelectList(
                await _bll.OwnerCompanies.GetAllAsync(), nameof(OwnerCompany.Id), nameof(OwnerCompany.Name));
            return View(vm);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _bll.Hotels.FirstOrDefaultAsync(id.Value);
            if (hotel == null)
            {
                return NotFound();
            }

            var vm = new HotelEntityVM
            {
                Hotel = hotel,
                OwnerCompanySelectList = new SelectList(
                    await _bll.OwnerCompanies.GetAllAsync(), nameof(OwnerCompany.Id), nameof(OwnerCompany.Name))
            };
            return View(vm);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, HotelEntityVM vm)
        {
            if (id != vm.Hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bll.Hotels.UpdateAsync(vm.Hotel);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            vm.OwnerCompanySelectList = new SelectList(
                await _bll.OwnerCompanies.GetAllAsync(), nameof(OwnerCompany.Id), nameof(OwnerCompany.Name));

            return View(vm);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _bll.Hotels.FirstOrDefaultAsync(id.Value);
            if (hotel == null)
            {
                return NotFound();
            }
            var vm = new HotelEntityVM
            {
                Hotel = hotel
            };
            
            return View(vm);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Hotels.RemoveAsync(id, User.UserId());
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
