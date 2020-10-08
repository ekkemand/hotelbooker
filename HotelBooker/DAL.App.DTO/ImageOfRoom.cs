using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class ImageOfRoom : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [MaxLength(80), Required] public string Name { get; set; } = default!;
        [MaxLength(4000), Required] public string Url { get; set; } = default!;
        [MaxLength(2000)] public string? Description { get; set; }
        
        [Required, Display(Name = "Hotel")] public Guid HotelId { get; set; }
        [JsonIgnore] public Hotel? Hotel { get; set; }
        
        [Required, Display(Name = "Room type")] public Guid RoomTypeId { get; set; }
        [JsonIgnore] public RoomType? RoomType { get; set; }
    }
}