#pragma warning disable 1591
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;
using HotelBooker.Helpers;

namespace HotelBooker.ViewModels
{
    public class HotelFilterHelperVM
    {
        public IEnumerable<Hotel> Hotels { get; set; } = default!;
        
        public HotelFilterData? FilterData { get; set; }
        
        public SelectList? OwnerCompanySelectList { get; set; }
        public SelectList? ConvenienceSelectList { get; set; }
        public SelectList? ReviewCategorySelectList { get; set; }
        public SelectList? AlphabeticalOrderOptions { get; set; }
        public SelectList? EstablishedDateOrderOptions { get; set; }
    }
}