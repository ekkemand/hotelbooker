using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using AppUser = BLL.App.DTO.Identity.AppUser;

namespace BLL.App.DTO
{
    public class Reservation : IDomainEntityId
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start date")]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End date")]
        public DateTime EndDateTime { get; set; }
        
        [Display(Name = "Number of rooms")]
        public int NumberOfRooms { get; set; }
        
        
        [Required, Display(Name = "Room type")] public Guid RoomTypeId { get; set; }
        [JsonIgnore] public RoomType? RoomType { get; set; }

        [Required] public Guid PersonId { get; set; }
        [JsonIgnore] public Person? Person { get; set; }
        
        public Guid? UserId { get; set; }
        [JsonIgnore] public AppUser? User { get; set; }

        [Required, Display(Name = "Hotel")] public Guid HotelId { get; set; }
        [JsonIgnore] public Hotel? Hotel { get; set; }

        [JsonIgnore] public ICollection<ReservationRow>? ReservationRows { get; set; }
    }
}