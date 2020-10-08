using System.Collections.Generic;
using BLL.App.DTO;
using BLL.App.DTO.HelperClasses;
using Contracts.BLL.App.HelperClasses;
using Microsoft.AspNetCore.Mvc.Rendering;

#pragma warning disable 1591

namespace HotelBooker.ViewModels
{
    public class HotelEntityVM
    {
        public Hotel Hotel { get; set; } = default!;
        public IEnumerable<GroupedConvenience>? GroupedConveniences { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public SelectList? OwnerCompanySelectList { get; set; }
    }
}