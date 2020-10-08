using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class ReservationRow : IDomainEntityId
    {
        public Guid Id { get; set; }

        [Required] public Guid ReservationId { get; set; }
        [JsonIgnore] public Reservation? Reservation { get; set; }
        
        [Required] public Guid ProductId { get; set; }
        [JsonIgnore] public Product? Product { get; set; }

        [JsonIgnore] public ICollection<ReservationRow>? ReservationRows { get; set; }
    }
}