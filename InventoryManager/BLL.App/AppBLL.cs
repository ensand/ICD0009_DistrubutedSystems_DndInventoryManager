using BLL.App.Services;
using com.enola.inventorymanager.BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork uow) : base(uow)
        {
        }

        public IArmorService Armors =>
            GetService<IArmorService>(() => new ArmorService(UnitOfWork));
        
        public IDndCharacterService DndCharacters =>
            GetService<IDndCharacterService>(() => new DndCharacterService(UnitOfWork));
        
        public IMagicalItemService MagicalItems =>
            GetService<IMagicalItemService>(() => new MagicalItemService(UnitOfWork));
        
        public IOtherEquipmentService OtherEquipments =>
            GetService<IOtherEquipmentService>(() => new OtherEquipmentService(UnitOfWork));
        
        public IWeaponService Weapons =>
            GetService<IWeaponService>(() => new WeaponService(UnitOfWork));
    }
}