using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class HotelConvenience : IDomainEntityId
    {
        public Guid Id { get; set; }
        [Required, Display(Name = "Hotel")] public Guid HotelId { get; set; }

        [JsonIgnore] public Hotel? Hotel { get; set; }

        [Required, Display(Name = "Convenience")] public Guid ConvenienceId { get; set; }
        [JsonIgnore] public Convenience? Convenience { get; set; }
    }
}