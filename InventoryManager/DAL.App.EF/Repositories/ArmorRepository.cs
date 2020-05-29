using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ArmorRepository : 
        EFBaseRepository<AppEntityTracker, Domain.Identity.AppUser, Domain.Armor, DAL.App.DTO.Armor>, 
        IArmorRepository
    {
        public ArmorRepository(AppEntityTracker repoEntityTracker) 
            : base(repoEntityTracker, new DAL.Base.Mappers.BaseMapper<Domain.Armor, DAL.App.DTO.Armor>())
        {
        }
    }
}