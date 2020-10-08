using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Convenience : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(80), Required]
        public string Name { get; set; } = default!;

        [MaxLength(2000)] public string? Description { get; set; }

        public Guid ConvenienceGroupId { get; set; }
        [JsonIgnore] public ConvenienceGroup? ConvenienceGroup { get; set; }
        [JsonIgnore] public ICollection<HotelConvenience>? HotelConveniences { get; set; }
        [JsonIgnore] public ICollection<RoomTypeConvenience>? RoomTypeConveniences { get; set; }
    }
}