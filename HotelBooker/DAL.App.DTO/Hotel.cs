using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Hotel : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(100), Required]
        public string Name { get; set; } = default!;

        [Range(1, 5), Required] public int Rating { get; set; }

        [MinLength(1), MaxLength(255), Required]
        public string Address { get; set; } = default!;
        
        [MaxLength(500), Required] public string Website { get; set; } = default!;
        [MaxLength(2000), Display(Name = "Image URL")] public string? ImageUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Established { get; set; }

        [Required] public Guid OwnerCompanyId { get; set; }

        [Display(Name = "Owner company")]
        [JsonIgnore]
        public OwnerCompany? OwnerCompany { get; set; }

        [JsonIgnore] public ICollection<RoomType>? RoomTypes { get; set; }
        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
        [JsonIgnore] public ICollection<Room>? Rooms { get; set; }
        [JsonIgnore] public ICollection<Reservation>? Reservations { get; set; }
        [JsonIgnore] public ICollection<ImageOfRoom>? ImageOfRooms { get; set; }
        [JsonIgnore] public ICollection<HotelConvenience>? HotelConveniences { get; set; }
        [JsonIgnore] public ICollection<Review>? Reviews { get; set; }
    }
}