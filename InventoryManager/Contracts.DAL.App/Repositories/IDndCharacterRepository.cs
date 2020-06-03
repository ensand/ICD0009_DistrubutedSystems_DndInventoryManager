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

    public interface IDndCharacterRepositoryCustom : IDndCharacterRepositoryCustom<DndCharacter, DndCharacterSummary> { }
    
    public interface IDndCharacterRepositoryCustom<TDndCharacter, TDndCharacterSummary>
    {
        Task<IEnumerable<TDndCharacterSummary>> CustomGetAllAsync(Guid? userId = default, bool noTracking = true);
        Task<TDndCharacter> CustomFirstOrDefaultAsync(Guid? id, Guid? userId = default, bool noTracking = true);
    }
}