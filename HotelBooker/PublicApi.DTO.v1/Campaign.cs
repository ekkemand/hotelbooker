using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using Domain;

namespace PublicApi.DTO.v1
{
    public class Campaign : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(100), Required]
        public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(5, 2)"), Display(Name = "Discount factor")]
        public decimal DiscountFactor { get; set; }

        [MaxLength(2000)] public string? Description { get; set; }
    }
}