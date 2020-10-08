#pragma warning disable 1591
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class ProductGroupsCreateEditVM
    {
        public ProductGroup ProductGroup { get; set; } = default!;
        
        public SelectList? ParentGroupSelectList { get; set; }
    }
}