using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using Domain;

namespace PublicApi.DTO.v1
{
    public class Hotel : IDomainEntityId
    {
        public Guid Id { get; set; }
        [MaxLength(100), Required] public string Name { get; set; } = default!;
        
        public int Rating { get; set; }
        
        [MaxLength(255), Required] public string Address { get; set; } = default!;

        public string Website { get; set; } = default!;
        
        public string? ImageUrl { get; set; }

        public DateTime Established { get; set; }

        public Guid OwnerCompanyId { get; set; }
        public string? OwnerCompanyName { get; set; }
    }
}