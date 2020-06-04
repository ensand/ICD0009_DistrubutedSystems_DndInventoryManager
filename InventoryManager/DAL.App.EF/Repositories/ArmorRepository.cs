using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class ArmorRepository : 
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.App.Armor, DAL.App.DTO.Armor>, 
        IArmorRepository
    {
        public ArmorRepository(AppDbContext repoDbContext) 
            : base(repoDbContext, new com.enola.inventorymanager.DAL.Base.Mappers.BaseMapper<Domain.App.Armor, DAL.App.DTO.Armor>())
        {
        }
    }
}