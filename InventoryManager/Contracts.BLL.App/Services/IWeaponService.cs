using BLL.App.DTO;
using com.enola.inventorymanager.Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IWeaponService : IBaseEntityService<Weapon>, IWeaponRepositoryCustom
    {
        
    }
}