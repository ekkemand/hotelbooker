using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Product : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(100), Required] public string Name { get; set; } = default!;
        
        [MaxLength(2000)] public string? Description { get; set; }

        [Display(Name = "Room type")]
        public Guid? RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        
        [Display(Name = "Product group")]
        public Guid ProductGroupId { get; set; }
        public ProductGroup? ProductGroup { get; set; }
        
        public ICollection<Price>? Prices { get; set; }
        public ICollection<ProductGroup>? ProductGroups { get; set; }
        public ICollection<ReservationRow>? ReservationRows { get; set; }
    }
}