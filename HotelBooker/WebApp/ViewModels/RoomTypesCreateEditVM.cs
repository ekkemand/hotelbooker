#pragma warning disable 1591
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.ViewModels
{
    public class RoomTypesCreateEditVM
    {
        public RoomType RoomType { get; set; } = default!;
        
        public SelectList? HotelSelectList { get; set; }
    }
}