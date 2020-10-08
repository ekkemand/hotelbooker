using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class ReservationRow : DomainEntityIdMetadata
    {
        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }
        
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
    }
}