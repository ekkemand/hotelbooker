using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class RoomType : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(80), Required]
        public string Type { get; set; } = default!;

        [MaxLength(2000)] public string? Description { get; set; }
        
        [Required, Display(Name = "Hotel")] public Guid HotelId { get; set; } = default!;
        [JsonIgnore] public Hotel? Hotel { get; set; }

        [JsonIgnore] public Product? Product { get; set; } // because one to optional one relationship
        [JsonIgnore] public ICollection<Room>? Rooms { get; set; }
        [JsonIgnore] public ICollection<Reservation>? Reservations { get; set; }
        [JsonIgnore] public ICollection<ImageOfRoom>? ImageOfRooms { get; set; }
        [JsonIgnore] public ICollection<RoomTypeConvenience>? RoomTypeConveniences { get; set; }
        [JsonIgnore] public ICollection<Review>? Reviews { get; set; }
    }
}