using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
    }

    public interface IDomainEntityId<TKey>
    where TKey: IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}