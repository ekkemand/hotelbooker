#pragma warning disable 1591
using System;
using System.Collections.Generic;
using BLL.App.DTO;

namespace HotelBooker.ViewModels
{
    public class ReservationDetailsVM
    {
        public Reservation Reservation { get; set; } = default!;

        public IEnumerable<ReservationRow>? ReservationRows { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        
        public Guid? DeletingRow { get; set; }

        public IEnumerable<Product> TakenProducts { get; set; } = default!;
        public IEnumerable<Price>? Prices { get; set; }
    }
}