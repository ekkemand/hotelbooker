using System;
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO.Identity;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Review : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Heading { get; set; } = default!;
        public string Content { get; set; } = default!;

        public Guid? RoomTypeId { get; set; }
        public Guid HotelId { get; set; }
        public Guid UserId { get; set; }
        public string? UserDisplayName { get; set; }
        public string? RoomTypeName { get; set; }
        public string? HotelName { get; set; }
        public string? NewCategoryString { get; set; }
        public Guid? ReviewCategoryId { get; set; }
        public ReviewCategory? ReviewCategory { get; set; }
    }
}