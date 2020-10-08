using System;
using BLL.App.DTO;

namespace Contracts.BLL.App.HelperClasses
{
    public class RoomTypeAdditionalData
    {
        public RoomType RoomType { get; set; } = default!;

        public int AvailableRooms { get; set; }
        public DateTime? EarliestReservation { get; set; }
    }
}