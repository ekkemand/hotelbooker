using System;

namespace PublicApi.DTO.v1
{
    public class HotelFilterData
    {
        public string? OwnerCompanyId { get; set; }
        
        public string? HotelConvenienceId { get; set; }
        
        public string? ReviewCategoryId { get; set; }
        
        public string? AlphabeticalOrder { get; set; }
        
        public string? DateEstablished { get; set; }
    }
}