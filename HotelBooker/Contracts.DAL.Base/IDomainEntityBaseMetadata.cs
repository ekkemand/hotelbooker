using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityBaseMetadata : IDomainEntityBaseMetadata<Guid>
    {
        
    }
    
    public interface IDomainEntityBaseMetadata<TKey> : IDomainEntityId<TKey>, IDomainEntityMetadata
        where TKey: IEquatable<TKey>
    {
        
    }
}