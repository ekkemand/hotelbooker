using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Reservation : DomainEntityIdMetadata
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDateTime { get; set; }
        
        public int NumberOfRooms { get; set; }
        
        public Guid RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }
        
        public Guid? UserId { get; set; }
        public AppUser? User { get; set; }

        public Guid PersonId { get; set; }
        public Person? Person { get; set; }

        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public ICollection<ReservationRow>? ReservationRows { get; set; }
    }
}