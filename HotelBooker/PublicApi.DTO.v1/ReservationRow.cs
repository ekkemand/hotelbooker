using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class ReservationRow : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public int RowNumber { get; set; }

        public Guid RoomTypeId { get; set; } = default!;
        public Guid ReservationId { get; set; } = default!;
        
        public Guid? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}