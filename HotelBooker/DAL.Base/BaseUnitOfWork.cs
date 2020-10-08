using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace DAL.Base
{
    public abstract class BaseUnitOfWork<TKey> : IBaseUnitOfWork, IBaseEntityTracker<TKey> 
        where TKey : IEquatable<TKey>
    {
        public abstract Task<int> SaveChangesAsync();

        private readonly Dictionary<Type, object> _repoCache = new Dictionary<Type, object>();
        
        private readonly Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> _entityTracker =
            new Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>>();
        
        public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod)
            where TRepository : class
        {
            if (_repoCache.TryGetValue(typeof(TRepository), out var repo))
            {
                return (TRepository) repo;
            }

            var newRepoInstance = repoCreationMethod();
            _repoCache.Add(typeof(TRepository), newRepoInstance);
            return newRepoInstance;
        }

        public void AddToEntityTracker(IDomainEntityId<TKey> internalEntity, IDomainEntityId<TKey> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }
        
        protected void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }
    }
}