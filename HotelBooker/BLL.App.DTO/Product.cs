using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Product : IDomainEntityId
    {
        [Display(Name = "Product")] public Guid Id { get; set; }

        [MinLength(1), MaxLength(100), Required]
        public string Name { get; set; } = default!;

        [MaxLength(2000)] public string? Description { get; set; }


        [Display(Name = "Room type")]
        public Guid? RoomTypeId { get; set; }
        [JsonIgnore] public RoomType? RoomType { get; set; }
        
        [Required, Display(Name = "Product Group")] public Guid ProductGroupId { get; set; }
        [JsonIgnore] public ProductGroup? ProductGroup { get; set; }

        [JsonIgnore] public ICollection<Price>? Prices { get; set; }
    }
}