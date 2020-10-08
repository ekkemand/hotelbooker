using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Room : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string RoomNumber { get; set; } = default!;
        public string? Description { get; set; }
        
        public string? HotelName { get; set; }
        public string? RoomTypeName { get; set; }

        public Guid HotelId { get; set; } = default!;
        public Guid RoomTypeId { get; set; } = default!;

    }
}