using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class ProductGroup : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(89), Required]
        public string Name { get; set; } = default!;
        [MaxLength(2000)] public string? Description { get; set; }
        
        public ICollection<Product>? Products { get; set; }
    }
}