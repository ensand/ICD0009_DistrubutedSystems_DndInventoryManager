using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class WeaponRepository : 
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.App.Weapon, DAL.App.DTO.Weapon>, 
        IWeaponRepository
    {
        public WeaponRepository(AppDbContext repoDbContext) 
            : base(repoDbContext, new DAL.Base.Mappers.BaseMapper<Domain.App.Weapon, DAL.App.DTO.Weapon>())
        {
        }
    }
}