using System;
using Microsoft.AspNetCore.Identity;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityUser<TUser> : IDomainEntityUser<Guid, TUser>
        where TUser : IdentityUser<Guid> 
    {
        
    }

    public interface IDomainEntityUser<TKey, TUser>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public TKey UserId { get; set; }
        public TUser? User { get; set; }
    }
}