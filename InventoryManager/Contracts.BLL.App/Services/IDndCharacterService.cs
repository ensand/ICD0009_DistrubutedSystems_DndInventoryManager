using BLL.App.DTO;
using com.enola.inventorymanager.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IDndCharacterService : IBaseEntityService<DndCharacter>, IDndCharacterRepositoryCustom<DndCharacter, DndCharacterSummary>
    {
    }
}