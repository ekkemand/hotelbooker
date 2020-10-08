using System;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppRole : IdentityRole<Guid>, IDomainEntityId
    {
        [MinLength(1)]
        [MaxLength(256)]
        
        public string DisplayName { get; set; } = default!;
    }
    
}