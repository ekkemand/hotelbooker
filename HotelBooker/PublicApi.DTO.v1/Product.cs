using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Product : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public Guid ProductGroupId { get; set; }
        public string? ProductGroupName { get; set; }
        public Guid? RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
    }
}