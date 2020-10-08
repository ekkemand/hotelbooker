using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Review : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(200)] public string Heading { get; set; } = default!;
        [MaxLength(4000)] public string Content { get; set; } = default!;

        public Guid? RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }

        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }

        public Guid? ReviewCategoryId { get; set; }
        public ReviewCategory? ReviewCategory { get; set; }
    }
}