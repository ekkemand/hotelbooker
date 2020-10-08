using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class OwnerCompany : IDomainEntityId
    {
        public Guid Id { get; set; }
        [MinLength(1), MaxLength(100), Required] public string Name { get; set; } = default!;

        [JsonIgnore] public ICollection<Hotel>? Hotels { get; set; }
    }
}