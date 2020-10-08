using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class OwnerCompany : IDomainEntityId
    {
        public Guid Id { get; set; }
        [MinLength(1), MaxLength(100)] public string Name { get; set; } = default!;
    }
}