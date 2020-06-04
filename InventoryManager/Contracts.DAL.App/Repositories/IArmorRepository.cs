using com.enola.inventorymanager.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IArmorRepository : IBaseRepository<Armor>, IArmorRepositoryCustom
    {
        
    }

    public interface IArmorRepositoryCustom : IArmorRepositoryCustom<Armor> { }
    
    public interface IArmorRepositoryCustom<TArmor> { }
}