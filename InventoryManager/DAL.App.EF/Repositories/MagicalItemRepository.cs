using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class MagicalItemRepository : 
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.App.MagicalItem, DAL.App.DTO.MagicalItem>, 
        IMagicalItemRepository
    {
        public MagicalItemRepository(AppDbContext repoDbContext) 
            : base(repoDbContext, new DAL.Base.Mappers.BaseMapper<Domain.App.MagicalItem, DAL.App.DTO.MagicalItem>())
        {
        }
    }
}