using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class HotelConvenience : DomainEntityIdMetadata
    {
        public Guid HotelId { get; set; }
        public Hotel? Hotel { get; set; }

        public Guid ConvenienceId { get; set; }
        public Convenience? Convenience { get; set; }
    }
}