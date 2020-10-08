using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Convenience : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(80), Required] public string Name { get; set; } = default!;
        [MaxLength(2000)] public string? Description { get; set; }

        public Guid ConvenienceGroupId { get; set; }
        public ConvenienceGroup? ConvenienceGroup { get; set; }

        public ICollection<HotelConvenience>? HotelConveniences { get; set; }
        public ICollection<RoomTypeConvenience>? RoomTypeConveniences { get; set; }
    }
}