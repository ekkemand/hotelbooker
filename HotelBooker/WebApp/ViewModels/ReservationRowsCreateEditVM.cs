#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class ReservationRowsCreateEditVM
    {
        public ReservationRow ReservationRow { get; set; } = default!;
        
        public SelectList? RoomTypeSelectList { get; set; }
        public SelectList? ReservationSelectList { get; set; }
    }
}