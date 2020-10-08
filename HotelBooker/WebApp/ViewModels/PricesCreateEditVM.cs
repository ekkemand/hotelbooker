#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class PricesCreateEditVM
    {
        public Price Price { get; set; } = default!;

        public SelectList? CurrencySelectList { get; set; }
        public SelectList? CampaignSelectList { get; set; }
        public SelectList? HotelSelectList { get; set; }
        public SelectList? ProductSelectList { get; set; }

        [Display(Name = "Or add a new currency")]
        public string? NewCurrency { get; set; }
    }
}