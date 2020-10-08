using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class Price : IDomainEntityId
    {
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(8, 2)")] public decimal Value { get; set; }

        [Display(Name = "Campaign")] public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        [Required, Display(Name = "Product")] public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        [Required, Display(Name = "Hotel")] public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        
        [Required, Display(Name = "Currency")] public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }
    }
}