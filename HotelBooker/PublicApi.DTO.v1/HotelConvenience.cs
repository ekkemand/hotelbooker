using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class HotelConvenience : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        
        public Guid HotelId { get; set; } = default!;
        public string? HotelName { get; set; }
        
        public Guid ConvenienceId { get; set; } = default!;
        public string? ConvenienceName { get; set; }
    }
}