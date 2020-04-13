using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IBaseRepository<Armor> Armors { get; }
        IBaseRepository<DndCharacter> DndCharacters { get; }
        IBaseRepository<MagicalItem> MagicalItems { get; }
        IBaseRepository<OtherEquipment> OtherEquipments { get; }
        IBaseRepository<Weapon> Weapons { get; }
    }
}