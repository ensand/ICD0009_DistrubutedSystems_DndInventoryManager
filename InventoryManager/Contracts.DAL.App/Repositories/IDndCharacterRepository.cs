using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDndCharacterRepository : IBaseRepository<DndCharacter>
    {
        Task<IEnumerable<DndCharacterSummary>> GetAllAsync(Guid? userId = default, bool noTracking = true);
        Task<DndCharacter> FirstOrDefaultAsync(Guid? id, Guid? userId = default, bool noTracking = true);
    }
}