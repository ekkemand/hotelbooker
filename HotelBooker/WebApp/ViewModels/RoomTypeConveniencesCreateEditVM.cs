#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class RoomTypeConveniencesCreateEditVM
    {
        public RoomTypeConvenience RoomTypeConvenience { get; set; } = default!;
        
        public SelectList? ConvenienceSelectList { get; set; }
        public SelectList? RoomTypeSelectList { get; set; }
    }
}