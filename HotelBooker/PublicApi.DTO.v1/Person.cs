using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;

namespace PublicApi.DTO.v1
{
    public class Person : IDomainEntityId
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string? NationalIdNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string? PhoneNumber { get; set; }
    }
}