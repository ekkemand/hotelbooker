using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using AppUser = DAL.App.DTO.Identity.AppUser;

namespace DAL.App.DTO
{
    public class Person : IDomainEntityId
    {
        public Guid Id { get; set; }

        [MaxLength(80), Required, Display(Name = "First name")]
        public string FirstName { get; set; } = default!;

        [MaxLength(80), Required, Display(Name = "Last name")]
        public string LastName { get; set; } = default!;

        [MaxLength(20), Display(Name = "National ID")] public string? NationalIdNumber { get; set; }

        [DataType(DataType.Date), Display(Name = "Birth date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [MaxLength(20), Display(Name = "Phone number")] public string? PhoneNumber { get; set; }

        [JsonIgnore] public ICollection<AppUser>? Users { get; set; }
        [JsonIgnore] public ICollection<Reservation>? Reservations { get; set; }

        [Display(Name = "Full name")]
        public string FirstLastName => FirstName + " " + LastName;
    }
}