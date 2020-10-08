using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Reservation : IDomainEntityId
    {
        public Guid Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }
        
        public int NumberOfRooms { get; set; }

        
        [Required] public Guid RoomTypeId { get; set; }
        [JsonIgnore] public RoomType? RoomType { get; set; }

        [Required] public Guid PersonId { get; set; }
        [JsonIgnore] public Person? Person { get; set; }
        
        public Guid? UserId { get; set; }
        [JsonIgnore] public AppUser? User { get; set; }

        [Required] public Guid HotelId { get; set; }
        [JsonIgnore] public Hotel? Hotel { get; set; }

        [JsonIgnore] public ICollection<ReservationRow>? ReservationRows { get; set; }
    }
}