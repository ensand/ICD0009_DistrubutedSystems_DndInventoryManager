using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class WeaponRepository : 
        EFBaseRepository<AppEntityTracker, Domain.Identity.AppUser, Domain.Weapon, DAL.App.DTO.Weapon>, 
        IWeaponRepository
    {
        public WeaponRepository(AppEntityTracker repoEntityTracker) 
            : base(repoEntityTracker, new DAL.Base.Mappers.BaseMapper<Domain.Weapon, DAL.App.DTO.Weapon>())
        {
        }
    }
}