using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Campaign : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(100), Required]
        public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(5, 2)"), Display(Name = "Discount factor")]
        public decimal DiscountFactor { get; set; }

        [MaxLength(2000)] public string? Description { get; set; }

        public ICollection<Price>? Prices { get; set; }
    }
}