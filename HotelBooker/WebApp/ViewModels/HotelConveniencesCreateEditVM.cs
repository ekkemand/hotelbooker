#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class HotelConveniencesCreateEditVM
    {
        public HotelConvenience HotelConvenience { get; set; } = default!;

        public SelectList? ConvenienceSelectList { get; set; }
        public SelectList? HotelSelectList { get; set; }
    }
}