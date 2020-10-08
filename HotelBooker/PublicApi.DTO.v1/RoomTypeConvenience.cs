using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class RoomTypeConvenience : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid RoomTypeId { get; set; } = default!;
        public string? RoomTypeName { get; set; }
        
        public Guid ConvenienceId { get; set; } = default!;
        public string? ConvenienceName { get; set; }
    }
}