using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class ReviewCategory : IDomainEntityId
    {
        public Guid Id { get; set; }
        [MaxLength(200)] public string Name { get; set; } = default!;
        public bool Accepted { get; set; } = false;

        [JsonIgnore] public ICollection<Review>? Reviews { get; set; }
    }
}