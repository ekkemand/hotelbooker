using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class HotelConvenience : IDomainEntityId
    {
        public Guid Id { get; set; }
        [Required] public Guid HotelId { get; set; }

        [JsonIgnore] public Hotel? Hotel { get; set; }

        [Required] public Guid ConvenienceId { get; set; }
        [JsonIgnore] public Convenience? Convenience { get; set; }
    }
}