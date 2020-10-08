
using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class RoomTypeDetailsDTO
    {
        public RoomType RoomType { get; set; } = default!;
        public IEnumerable<GroupedConvenienceDTO>? GroupedConveniences { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public Product? Product { get; set; }
        public IEnumerable<Price>? Prices { get; set; }
        
        public int? AvailableRooms { get; set; }
        public DateTime? EarliestReservation { get; set; }
    }
}