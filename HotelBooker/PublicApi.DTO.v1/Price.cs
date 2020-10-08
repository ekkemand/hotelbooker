using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Price : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public decimal Value { get; set; }
        
        public Guid? CampaignId { get; set; }
        public Campaign? Campaign { get; set; }

        public Guid CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public string? NewCurrency { get; set; }
        
        public Guid ProductId { get; set; } = default!;
        public Product? Product { get; set; }
        
        public Guid HotelId { get; set; } = default!;
        public string? HotelName { get; set; }
    }
}