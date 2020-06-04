using com.enola.inventorymanager.Contracts.DAL.Base;
using Contracts.DAL.App.Repositories;

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