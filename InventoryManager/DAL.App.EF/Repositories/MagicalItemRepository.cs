using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class MagicalItemRepository : 
        EFBaseRepository<AppEntityTracker, Domain.Identity.AppUser, Domain.MagicalItem, DAL.App.DTO.MagicalItem>, 
        IMagicalItemRepository
    {
        public MagicalItemRepository(AppEntityTracker repoEntityTracker) 
            : base(repoEntityTracker, new DAL.Base.Mappers.BaseMapper<Domain.MagicalItem, DAL.App.DTO.MagicalItem>())
        {
        }
    }
}