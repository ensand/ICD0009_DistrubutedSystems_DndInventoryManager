using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
    {
        IArmorRepository Armors { get; }
        IDndCharacterRepository DndCharacters { get; }
        IMagicalItemRepository MagicalItems { get; }
        IOtherEquipmentRepository OtherEquipments { get; }
        IWeaponRepository Weapons { get; }
    }
}