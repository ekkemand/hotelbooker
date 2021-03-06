﻿using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class ImageOfRoom : DomainEntityIdMetadata
    {

        [MaxLength(80)] public string Name { get; set; } = default!;
        [MaxLength(4000)] public string Url { get; set; } = default!;
        [MaxLength(2000)] public string? Description { get; set; }
        
        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        
        public Guid RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
    }
}