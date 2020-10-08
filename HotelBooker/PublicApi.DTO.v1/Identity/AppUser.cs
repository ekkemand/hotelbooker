using System;
using System.Collections.Generic;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; } = default!;
        
        public string DisplayName { get; set; } = default!;

        public Guid PersonId { get; set; } = default!;

        public Person? Person { get; set; }

        public IEnumerable<string>? Roles { get; set; }
    }
}