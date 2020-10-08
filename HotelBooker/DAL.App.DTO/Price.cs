using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class Price : IDomainEntityId
    {
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(8, 2)")] public decimal Value { get; set; }

        public Guid? CampaignId { get; set; }
        [JsonIgnore] public Campaign? Campaign { get; set; }

        [Required] public Guid ProductId { get; set; }
        [JsonIgnore] public Product? Product { get; set; }

        [Required] public Guid HotelId { get; set; }
        [JsonIgnore] public Hotel? Hotel { get; set; }
        
        [Required] public Guid CurrencyId { get; set; }
        [JsonIgnore] public Currency? Currency { get; set; }
    }
}