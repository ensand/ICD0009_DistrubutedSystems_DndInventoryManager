using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDndCharacterRepository : IBaseRepository<DndCharacter>, IDndCharacterRepositoryCustom
    {
        
    }

    public interface IDndCharacterRepositoryCustom : IDndCharacterRepositoryCustom<DndCharacterSummary> { }
    
    public interface IDndCharacterRepositoryCustom<TDndCharacterSummary>
    {
        Task<IEnumerable<TDndCharacterSummary>> CustomGetAllAsync(Guid? userId = default, bool noTracking = true);
        Task<DndCharacter> CustomFirstOrDefaultAsync(Guid? id, Guid? userId = default, bool noTracking = true);
    }
}