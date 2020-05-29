using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IWeaponRepository : IBaseRepository<Weapon>, IWeaponRepositoryCustom
    {
        
    }
    
    public interface IWeaponRepositoryCustom
    {
        
    }
}