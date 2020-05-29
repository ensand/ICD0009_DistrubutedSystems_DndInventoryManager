using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOtherEquipmentRepository : IBaseRepository<OtherEquipment>, IOtherEquipmentRepositoryCustom
    {
        
    }
    
    public interface IOtherEquipmentRepositoryCustom
    {
        
    }
}