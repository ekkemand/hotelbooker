#pragma warning disable 1591
using System.ComponentModel.DataAnnotations;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.ViewModels
{
    public class ReviewsCreateEditVM
    {
        public Review Review { get; set; } = default!;

        [MaxLength(200), Display(Name = "Or add a new category")] public string? CustomCategoryName { get; set; }

        public SelectList? ReviewCategorySelectList { get; set; }
    }
}