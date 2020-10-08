#pragma warning disable 1591
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DAL.App.EF;
using Domain.App.Identity;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelBooker.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public IndexModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string Username { get; set; } = default!;

        [TempData] public string? StatusMessage { get; set; }

        [BindProperty] public InputModel Input { get; set; } = default!;

        public class InputModel
        {
            [MaxLength(80), Display(Name = "First name")]
            public string FirstName { get; set; } = default!;

            [MaxLength(80), Display(Name = "Last name")]
            public string LastName { get; set; } = default!;

            [MaxLength(100), Display(Name = "Display name")]
            public string DisplayName { get; set; } = default!;

            [DataType(DataType.Date), Display(Name = "Birth date")]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public DateTime BirthDate { get; set; }

            [MaxLength(20), Display(Name = "National ID")]
            public string? NationalIdNumber { get; set; }

            [Phone]
            [MaxLength(20), Display(Name = "Phone number")]
            public string? PhoneNumber { get; set; }
        }


        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var person = user.Person!;

            Username = userName;

            Input = new InputModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                DisplayName = user.DisplayName,
                BirthDate = person.BirthDate,
                PhoneNumber = person.PhoneNumber,
                NationalIdNumber = person.NationalIdNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = _context.Users.Include(x => x.Person)
                .SingleOrDefaultAsync(x => x.Id == User.UserId()).Result;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public AppUser UpdateUserFields(AppUser user)
        {
            user.Person!.FirstName = Input.FirstName;
            user.Person!.LastName = Input.LastName;
            user.DisplayName = Input.DisplayName;
            user.Person!.BirthDate = Input.BirthDate;
            user.Person!.PhoneNumber = Input.PhoneNumber;
            user.Person!.NationalIdNumber = Input.NationalIdNumber;

            return user;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = _context.Users.Include(x => x.Person)
                .SingleOrDefaultAsync(x => x.Id == User.UserId()).Result;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            _context.Users.Update(UpdateUserFields(user));
            await _context.SaveChangesAsync();
            
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}