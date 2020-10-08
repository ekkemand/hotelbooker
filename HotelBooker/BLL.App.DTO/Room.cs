using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Room : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(10), Required, Display(Name = "Room number")]
        public string RoomNumber { get; set; } = default!;

        [MaxLength(2000)] public string? Description { get; set; }

        [Required, Display(Name = "Hotel")] public Guid HotelId { get; set; }
        [JsonIgnore] public Hotel? Hotel { get; set; }

        [Required, Display(Name = "Room type")] public Guid RoomTypeId { get; set; }
        [JsonIgnore, Display(Name = "Room type")] public RoomType? RoomType { get; set; }

    }
}