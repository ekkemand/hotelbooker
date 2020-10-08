using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<Guid, TEntity> 
        where TEntity : class, IDomainEntityId<Guid>, new()
    {
    }

    public interface IBaseRepository<in TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Limit the result to this users data (entity.userId == userid)</param>
        /// <param name="noTracking">use AsNotracking if datasource supports it</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(object? userId = null, bool noTracking = true);
        Task<TEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true);
        TEntity Add(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, object? userId = null);
        Task<TEntity> RemoveAsync(TEntity entity, object? userId = null);
        Task<TEntity> RemoveAsync(TKey id, object? userId = null);
        Task<bool> ExistsAsync(TKey id, object? userId = null);

    }
}