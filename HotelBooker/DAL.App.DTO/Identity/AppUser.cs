using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using System.Text.Json.Serialization;

namespace DAL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId
    {
        public Guid Id { get; set; }
        
        [EmailAddress, MaxLength(256)]
        public string Email { get; set; } = default!;
        
        [MaxLength(100), Display(Name = "Display name")] public string DisplayName { get; set; } = default!;
        [Required] public Guid PersonId { get; set; }

        [JsonIgnore] public Person? Person { get; set; }

        [JsonIgnore] public ICollection<Reservation>? Reservations { get; set; }
        [JsonIgnore] public ICollection<Review>? Reviews { get; set; }
    }
}