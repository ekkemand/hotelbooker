using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.Base.Mappers;
using Contracts.BLL.Base.Services;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace BLL.Base.Services
{
    public class BaseEntityService<TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity> :
        BaseEntityService<Guid, TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity>,
        IBaseEntityService<TBLLEntity>
        where TDALEntity : class, IDomainEntityId<Guid>, new()
        where TBLLEntity : class, IDomainEntityId<Guid>, new()
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
        where TRepository : IBaseRepository<Guid, TDALEntity>
        where TMapper : IBaseMapper<TDALEntity, TBLLEntity>
    {
        public BaseEntityService(TUnitOfWork uow, TRepository repository, TMapper mapper)
            : base(uow, repository, mapper)
        {
        }
    }

    public class
        BaseEntityService<TKey, TUnitOfWork, TRepository, TMapper, TDALEntity,
            TBLLEntity> : IBaseEntityService<TKey, TBLLEntity>
        where TKey : IEquatable<TKey>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TBLLEntity : class, IDomainEntityId<TKey>, new()
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TRepository : IBaseRepository<TKey, TDALEntity>
        where TMapper : IBaseMapper<TDALEntity, TBLLEntity>

    {
        protected readonly TUnitOfWork UnitOfWork;
        protected TMapper Mapper;
        protected readonly TRepository Repository;

        public BaseEntityService(TUnitOfWork unitOfWork, TRepository repository,
            TMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Repository = repository;
            // ServiceRepository = ServiceUnitOfWork.GetRepository<IBaseRepository<TDALEntity>>();
        }

        public virtual async Task<IEnumerable<TBLLEntity>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var dalEntities = await Repository.GetAllAsync(userId, noTracking);
            var result = dalEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public virtual async Task<TBLLEntity> FirstOrDefaultAsync(TKey id, object? userId = null,
            bool noTracking = true)
        {
            var dalEntity = await Repository.FirstOrDefaultAsync(id, userId, noTracking);
            var result = Mapper.Map(dalEntity);
            return result;
        }

        public virtual TBLLEntity Add(TBLLEntity entity)
        {
            var dalEntity = Mapper.Map(entity);
            var trackedDALEntity = Repository.Add(dalEntity);
            UnitOfWork.AddToEntityTracker(trackedDALEntity, entity);
            var result = Mapper.Map(trackedDALEntity);

            return result;
        }

        public virtual async Task<TBLLEntity> UpdateAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.UpdateAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.RemoveAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var resultDALEntity = await Repository.RemoveAsync(id, userId);
            var result = Mapper.Map(resultDALEntity);
            return result;
        }

        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var result = await Repository.ExistsAsync(id, userId);
            return result;
        }
    }
}