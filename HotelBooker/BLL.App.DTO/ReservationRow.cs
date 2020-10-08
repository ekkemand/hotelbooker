using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ReservationRow : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [Display(Name = "Product")]
        public Guid? ProductId { get; set; }
        [JsonIgnore] public Product? Product { get; set; }
        
        [Required, Display(Name = "Reservation")] public Guid ReservationId { get; set; }
        [JsonIgnore] public Reservation? Reservation { get; set; }
    }
}