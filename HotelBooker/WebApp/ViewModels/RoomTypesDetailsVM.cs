#pragma warning disable 1591
using System;
using System.Collections.Generic;
using BLL.App.DTO;
using BLL.App.DTO.HelperClasses;
using Contracts.BLL.App.HelperClasses;

namespace HotelBooker.ViewModels
{
    public class RoomTypesDetailsVM
    {
        public RoomType RoomType { get; set; } = default!;

        public IEnumerable<GroupedConvenience>? GroupedConveniences { get; set; }
        public int AvailableRooms { get; set; }
        public DateTime? EarliestReservation { get; set; }
    }
}