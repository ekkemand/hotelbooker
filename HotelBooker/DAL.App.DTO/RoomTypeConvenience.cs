using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace DAL.App.DTO
{
    public class RoomTypeConvenience : IDomainEntityId
    {
        public Guid Id { get; set; }
        [Required] public Guid RoomTypeId { get; set; }
        [JsonIgnore] public RoomType? RoomType { get; set; }
        
        [Required] public Guid ConvenienceId { get; set; }
        [JsonIgnore] public Convenience? Convenience { get; set; }
        
    }
}