using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ConvenienceGroup : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(80), Required]
        public string Name { get; set; } = default!;

        [MaxLength(2000)] public string? Description { get; set; }

        [JsonIgnore] public ICollection<RoomTypeConvenience>? RoomTypeConveniences { get; set; }
        [JsonIgnore] public ICollection<Convenience>? Conveniences { get; set; }
    }
}