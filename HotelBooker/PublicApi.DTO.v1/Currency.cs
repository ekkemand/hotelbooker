using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Currency : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
    }
}