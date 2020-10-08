using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Room : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(10), Required] public string RoomNumber { get; set; } = default!;
        [MaxLength(2000)] public string? Description { get; set; }

        
        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        
        public Guid RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
    }
}