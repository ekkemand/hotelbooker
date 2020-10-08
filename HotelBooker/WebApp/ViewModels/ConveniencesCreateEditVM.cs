#pragma warning disable 1591
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.ViewModels
{
    public class ConveniencesCreateEditVM
    {
        public Convenience Convenience { get; set; } = default!;
        
        public SelectList? ConvenienceGroupSelectList { get; set; }
    }
}