﻿using System;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class ConvenienceGroup : IDomainEntityId
    {
        public Guid Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}