using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class RoomType : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string? Description { get; set; }
        
        public Guid HotelId { get; set; } = default!;
        public string? HotelName { get; set; }
    }
}