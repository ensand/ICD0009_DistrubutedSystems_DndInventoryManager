using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDndCharacterRepository : IDndCharacterRepository<Guid, DndCharacter>, IBaseRepository<DndCharacter>
    {
    }
    
    public interface IDndCharacterRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        // Task<IEnumerable<TDALEntity>> AllAsync(TKey userId = default, bool noTracking = true);
        // Task<TDALEntity> FirstOrDefaultAsync(TKey id, TKey userId = default, bool noTracking = true);
        //
        // Task<TDALEntity> RemoveAsync(TDALEntity entity, TKey userId = default);
        // Task<TDALEntity> RemoveAsync(TKey id, TKey userId = default);
    }
}