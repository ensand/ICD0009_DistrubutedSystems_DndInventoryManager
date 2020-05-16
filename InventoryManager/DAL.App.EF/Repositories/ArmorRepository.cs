using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ArmorRepository : EFBaseRepository<AppDbContext, Domain.Armor, DAL.App.DTO.Armor>, IArmorRepository
    {
        public ArmorRepository(AppDbContext dbContext) 
            : base(dbContext, new BaseDALMapper<Domain.Armor, DAL.App.DTO.Armor>())
        {
        }
    }
}