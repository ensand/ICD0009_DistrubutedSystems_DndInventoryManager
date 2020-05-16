using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class WeaponRepository : EFBaseRepository<AppDbContext, Domain.Weapon, DAL.App.DTO.Weapon>, IWeaponRepository
    {
        public WeaponRepository(AppDbContext dbContext) 
            : base(dbContext, new BaseDALMapper<Domain.Weapon, DAL.App.DTO.Weapon>())
        {
        }
    }
}