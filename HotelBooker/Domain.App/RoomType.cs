using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class RoomType : DomainEntityIdMetadata
    {
        [MaxLength(80), Required] public string Type { get; set; } = default!;
        [MaxLength(2000)] public string? Description { get; set; }

        public Product? Product { get; set; }
        
        [Display(Name = "Hotel")] public Guid HotelId { get; set; } = default!;
        public Hotel? Hotel { get; set; }

        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<ImageOfRoom>? ImageOfRooms { get; set; }
        public ICollection<RoomTypeConvenience>? RoomTypeConveniences { get; set; }
    }
}