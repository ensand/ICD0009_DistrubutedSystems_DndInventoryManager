using com.enola.inventorymanager.Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IMagicalItemRepository : IBaseRepository<MagicalItem>, IMagicalItemRepositoryCustom
    {
        
    }
    
    public interface IMagicalItemRepositoryCustom : IMagicalItemRepositoryCustom<MagicalItem> { }
    
    public interface IMagicalItemRepositoryCustom<TMagicalItem> { }
}