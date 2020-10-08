using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Review : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(200)] public string Heading { get; set; } = default!;
        [MaxLength(4000)] public string Content { get; set; } = default!;

        [Display(Name = "Room type")]
        public Guid? RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }

        [Display(Name = "Hotel")]
        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        
        [Display(Name = "User")]
        public Guid UserId { get; set; }
        public AppUser? User { get; set; }

        [Display(Name = "Category")]
        public Guid? ReviewCategoryId { get; set; }
        public ReviewCategory? ReviewCategory { get; set; }
    }
}