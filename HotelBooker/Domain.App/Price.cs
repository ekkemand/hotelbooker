using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Price : DomainEntityIdMetadata
    {
        [Column(TypeName = "decimal(8, 2)")] public decimal Value { get; set; }

        public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        
        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }
    }
}