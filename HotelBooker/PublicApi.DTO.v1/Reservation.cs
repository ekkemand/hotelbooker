using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
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

        public Guid PersonId { get; set; } = default!;
        
        public Guid HotelId { get; set; } = default!;
        public Hotel? Hotel { get; set; }
        
        public Guid RoomTypeId { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
    }
}