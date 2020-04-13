using Contracts.DAL.App;
using Contracts.DAL.Base.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }


        public IBaseRepository<Armor> Armors =>
            GetRepository<IBaseRepository<Armor>>(() => new EFBaseRepository<Armor, AppDbContext>(UowDbContext));
        
        public IBaseRepository<DndCharacter> DndCharacters =>
            GetRepository<IBaseRepository<DndCharacter>>(() => new EFBaseRepository<DndCharacter, AppDbContext>(UowDbContext));
        
        public IBaseRepository<MagicalItem> MagicalItems  =>
            GetRepository<IBaseRepository<MagicalItem>>(() => new EFBaseRepository<MagicalItem, AppDbContext>(UowDbContext));
        
        public IBaseRepository<OtherEquipment> OtherEquipments  =>
            GetRepository<IBaseRepository<OtherEquipment>>(() => new EFBaseRepository<OtherEquipment, AppDbContext>(UowDbContext));
        
        public IBaseRepository<Weapon> Weapons  =>
            GetRepository<IBaseRepository<Weapon>>(() => new EFBaseRepository<Weapon, AppDbContext>(UowDbContext));
    }
}