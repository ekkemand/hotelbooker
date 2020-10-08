using System;
using System.ComponentModel.DataAnnotations;

#pragma warning disable 1591

namespace HotelBooker.Helpers
{
    public class HotelFilterData
    {
        [Display(Name = "Owners")]
        public Guid? OwnerCompanyId { get; set; }
        
        [Display(Name = "Conveniences")]
        public Guid? HotelConvenienceId { get; set; }
        
        [Display(Name = "Review categories")]
        public Guid? ReviewCategoryId { get; set; }
        
        [Display(Name = "Order by name")]
        public string? AlphabeticalOrder { get; set; }
        
        [Display(Name = "Order by date established")]
        public string? DateEstablished { get; set; }
    }
}