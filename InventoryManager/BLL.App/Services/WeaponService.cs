using BLL.App.Mappers;
using com.enola.inventorymanager.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class WeaponService : BaseEntityService<IAppUnitOfWork, IWeaponRepository, IWeaponMapper, DAL.App.DTO.Weapon, BLL.App.DTO.Weapon>, IWeaponService
    {
        public WeaponService(IAppUnitOfWork uow) : base(uow, uow.Weapons, new WeaponMapper())
        {
        }
    }
}