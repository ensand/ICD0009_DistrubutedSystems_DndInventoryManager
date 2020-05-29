using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<AppEntityTracker>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppEntityTracker uowEntityTracker) : base(uowEntityTracker)
        {
        }

        public IArmorRepository Armors =>
            GetRepository<IArmorRepository>(() => new ArmorRepository(UowEntityTracker));
        
        public IDndCharacterRepository DndCharacters =>
            GetRepository<IDndCharacterRepository>(() => new DndCharacterRepository(UowEntityTracker));
        
        public IMagicalItemRepository MagicalItems =>
            GetRepository<IMagicalItemRepository>(() => new MagicalItemRepository(UowEntityTracker));
        
        public IOtherEquipmentRepository OtherEquipments =>
            GetRepository<IOtherEquipmentRepository>(() => new OtherEquipmentRepository(UowEntityTracker));
        
        public IWeaponRepository Weapons =>
            GetRepository<IWeaponRepository>(() => new WeaponRepository(UowEntityTracker));
    }
}