#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class ImageOfRoomsCreateEditVM
    {
        public ImageOfRoom ImageOfRoom { get; set; } = default!;
        
        public SelectList? HotelSelectList { get; set; }
        public SelectList? RoomTypeSelectList { get; set; }
    }
}