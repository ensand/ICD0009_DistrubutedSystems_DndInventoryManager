using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class MagicalItemRepository : EFBaseRepository<AppDbContext, Domain.MagicalItem, DAL.App.DTO.MagicalItem>, IMagicalItemRepository
    {
        public MagicalItemRepository(AppDbContext dbContext) 
            : base(dbContext, new BaseDALMapper<Domain.MagicalItem, DAL.App.DTO.MagicalItem>())
        {
        }
    }
}