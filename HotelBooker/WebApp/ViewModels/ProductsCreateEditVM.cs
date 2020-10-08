#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class ProductsCreateEditVM
    {
        public Product Product { get; set; } = default!;
        
        public SelectList? RoomTypeSelectList { get; set; }
        public SelectList? ProductGroupSelectList { get; set; }
    }
}