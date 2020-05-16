using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public IArmorRepository Armors =>
            GetRepository<IArmorRepository>(() => new ArmorRepository(UowDbContext));
        
        public IDndCharacterRepository DndCharacters =>
            GetRepository<IDndCharacterRepository>(() => new DndCharacterRepository(UowDbContext));
        
        public IMagicalItemRepository MagicalItems =>
            GetRepository<IMagicalItemRepository>(() => new MagicalItemRepository(UowDbContext));
        
        public IOtherEquipmentRepository OtherEquipments =>
            GetRepository<IOtherEquipmentRepository>(() => new OtherEquipmentRepository(UowDbContext));
        
        public IWeaponRepository Weapons =>
            GetRepository<IWeaponRepository>(() => new WeaponRepository(UowDbContext));
    }
}