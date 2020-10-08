using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Person : DomainEntityIdMetadata
    {
        [MaxLength(80), Required, Display(Name = "First name")]
        public string FirstName { get; set; } = default!;

        [MaxLength(80), Required, Display(Name = "Last name")]
        public string LastName { get; set; } = default!;

        [MaxLength(20), Display(Name = "National ID")] public string? NationalIdNumber { get; set; }

        [DataType(DataType.Date), Display(Name = "Birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [MaxLength(20), Display(Name = "Phone number")] public string? PhoneNumber { get; set; }

        public ICollection<AppUser>? Users { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }

        [Display(Name = "Full name")]
        public string FirstLastName => FirstName + " " + LastName;
    }
}