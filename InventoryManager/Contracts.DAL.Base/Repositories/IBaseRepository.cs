using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity> : IBaseRepository<Guid, TEntity>
        where TEntity : class, IDomainEntityId<Guid>, new()
    {
    }

    public interface IBaseRepository<in TKey, TEntity>
        where TEntity : class, IDomainEntityId<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Get all entities that belong to the given user.
        /// </summary>
        /// <param name="userId">Limit the result to this users data.</param>
        /// <param name="noTracking">Use AsNoTracking if data source supports it.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(TKey userId = default, bool noTracking = true);

        /// <summary>
        /// Find the entity by given ID or return the default result.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <param name="userId">Limit the result to this users data.</param>
        /// <param name="noTracking">Use AsNoTracking if data source supports it.</param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultAsync(TKey id, TKey userId = default, bool noTracking = true);
        
        TEntity Add(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity, TKey userId = default);

        Task<TEntity> RemoveAsync(TEntity entity, TKey userId = default);
        Task<TEntity> RemoveAsync(TKey id, TKey userId = default);

        Task<bool> ExistsAsync(TKey id, TKey userId = default);
    }
}