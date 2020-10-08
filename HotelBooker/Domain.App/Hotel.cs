using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Hotel : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(100), Required]
        public string Name { get; set; } = default!;

        [Range(1, 5)] public int Rating { get; set; }

        [MinLength(1), MaxLength(255)]
        public string Address { get; set; } = default!;
        [MaxLength(500)] public string Website { get; set; } = default!;
        [MaxLength(2000), Display(Name = "Image URL")] public string? ImageUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Established { get; set; }

        public Guid OwnerCompanyId { get; set; }

        [Display(Name = "Owner company")] public OwnerCompany? OwnerCompany { get; set; }

        public ICollection<RoomType>? RoomTypes { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Price>? Prices { get; set; }
        public ICollection<Room>? Rooms { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<ImageOfRoom>? ImageOfRooms { get; set; }
        public ICollection<HotelConvenience>? HotelConveniences { get; set; }
    }
}