using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class RoomTypeConvenience : DomainEntityIdMetadata
    {
        
        public Guid RoomTypeId { get; set; }
        public RoomType? RoomType { get; set; }

        
        public Guid ConvenienceId { get; set; }
        public Convenience? Convenience { get; set; }
    }
}