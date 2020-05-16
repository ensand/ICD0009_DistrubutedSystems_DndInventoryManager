using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class WeaponService : BaseEntityService<IWeaponRepository, IAppUnitOfWork, DAL.App.DTO.Weapon, BLL.App.DTO.Weapon>, IWeaponService
    {
        public WeaponService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Weapon, BLL.App.DTO.Weapon>(), unitOfWork.Weapons)
        {
        }
    }
}