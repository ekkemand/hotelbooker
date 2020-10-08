using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class ProductGroup : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(89), Required]
        public string Name { get; set; } = default!;

        [MaxLength(2000)] public string? Description { get; set; }

        [JsonIgnore] public ICollection<Product>? Products { get; set; }
    }
}