using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class DndCharacterService : BaseEntityService<IAppUnitOfWork, IDndCharacterRepository, IDndCharacterMapper, DAL.App.DTO.DndCharacter, BLL.App.DTO.DndCharacter>, IDndCharacterService
    {

        public DndCharacterService(IAppUnitOfWork uow) : base(uow, uow.DndCharacters, new DndCharacterMapper())
        {
            
        }

        public async Task<IEnumerable<DndCharacterSummary>> CustomGetAllAsync(Guid? userId = default, bool noTracking = true)
        {
            return (await Repository.CustomGetAllAsync(userId, noTracking)).Select(e => Mapper.MapDndCharacterSummary(e));
        }

        public async Task<DndCharacter> CustomFirstOrDefaultAsync(Guid? id, Guid? userId = default, bool noTracking = true)
        {
            var dalEntity = await Repository.CustomFirstOrDefaultAsync(id, userId, noTracking);
            
            return Mapper.MapDndCharacter(dalEntity);
        }
    }
}