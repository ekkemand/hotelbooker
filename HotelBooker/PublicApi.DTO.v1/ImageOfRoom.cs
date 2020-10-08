using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class ImageOfRoom : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        public string Url { get; set; } = default!;
        public string? Description { get; set; }
        
        public Guid HotelId { get; set; }
        public string? HotelName { get; set; }
        
        public Guid RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
    }
}