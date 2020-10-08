using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class RoomTypeConvenience : IDomainEntityId
    {
        public Guid Id { get; set; }
        [Required, Display(Name = "Room type")] public Guid RoomTypeId { get; set; }
        [JsonIgnore] public RoomType? RoomType { get; set; }

        [Required, Display(Name = "Convenience")] public Guid ConvenienceId { get; set; }
        [JsonIgnore] public Convenience? Convenience { get; set; }
    }
}