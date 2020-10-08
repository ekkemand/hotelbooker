using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Convenience : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        
        public Guid ConvenienceGroupId { get; set; } = default!;
        public string? ConvenienceGroupName { get; set; }
    }
}