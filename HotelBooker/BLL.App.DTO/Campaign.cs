using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Campaign : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MinLength(1), MaxLength(100), Required]
        public string Name { get; set; } = default!;

        [Column(TypeName = "decimal(5, 2)"), Display(Name = "Discount factor")]
        public decimal DiscountFactor { get; set; }

        [MaxLength(2000)] public string? Description { get; set; }

        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
    }
}