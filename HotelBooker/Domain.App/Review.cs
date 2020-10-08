using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Review : DomainEntityIdMetadataUser<AppUser>
    {
        [MaxLength(200)] public string Heading { get; set; } = default!;
        [MaxLength(4000)] public string Content { get; set; } = default!;
        
        public Guid? RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        
        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public Guid? ReviewCategoryId { get; set; }
        public ReviewCategory? ReviewCategory { get; set; }
    }
}