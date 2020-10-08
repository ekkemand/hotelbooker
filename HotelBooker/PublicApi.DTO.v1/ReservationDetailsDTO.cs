
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class ReservationDetailsDTO
    {
        public Reservation Reservation { get; set; } = default!;

        public IEnumerable<ReservationRow>? ReservationRows { get; set; }
        
        public IEnumerable<Product> TakenProducts { get; set; } = default!;
        public IEnumerable<Price>? Prices { get; set; }
    }
}