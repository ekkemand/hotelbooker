#pragma warning disable 1591
using System.Collections.Generic;
using BLL.App.DTO;
using Domain.App.Identity;

namespace HotelBooker.ViewModels
{
    public class UsersDetailsVM
    {
        public AppUser User { get; set; } = default!;
        public Person Person { get; set; } = default!;
        
        public IList<string>? RolesList { get; set; }
    }
}