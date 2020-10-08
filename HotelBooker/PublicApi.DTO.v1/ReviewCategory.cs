using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class ReviewCategory : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Accepted { get; set; } = false;
    }
}