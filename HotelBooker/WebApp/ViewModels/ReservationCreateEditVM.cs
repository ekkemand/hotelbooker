#pragma warning disable 1591
using System;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.ViewModels
{
    public class ReservationCreateEditVM
    {
        public Reservation Reservation { get; set; } = default!;
        public int MaxRooms { get; set; }
        public DateTime MinDate { get; set; }

        public string? Error { get; set; }
        
        public SelectList? HotelSelectList { get; set; }
        public SelectList? RoomCountSelectList { get; set; }
        public SelectList? RoomTypeSelectList { get; set; }
    }
}