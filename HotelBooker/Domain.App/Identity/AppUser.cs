using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>, IDomainEntityId
    {
        // Add your own fields
        [MaxLength(100), Display(Name = "Display name")] public string DisplayName { get; set; } = default!;
        
        public Guid PersonId { get; set; } = default!;
        public Person? Person { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        
        public string? CreatedBy { get; set; }
        public string? ChangedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}