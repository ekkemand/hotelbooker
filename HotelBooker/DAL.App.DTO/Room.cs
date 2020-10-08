using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Room : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(10), Required]
        public string RoomNumber { get; set; } = default!;

        [MaxLength(2000)] public string? Description { get; set; }

        [Required] public Guid HotelId { get; set; }
        [JsonIgnore] public Hotel? Hotel { get; set; }

        [Required] public Guid RoomTypeId { get; set; }
        [JsonIgnore] public RoomType? RoomType { get; set; }
    }
}