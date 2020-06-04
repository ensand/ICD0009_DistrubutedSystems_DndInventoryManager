using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class OtherEquipmentRepository : 
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.App.OtherEquipment, DAL.App.DTO.OtherEquipment>, 
        IOtherEquipmentRepository
    {
        public OtherEquipmentRepository(AppDbContext repoDbContext) 
            : base(repoDbContext, new com.enola.inventorymanager.DAL.Base.Mappers.BaseMapper<Domain.App.OtherEquipment, DAL.App.DTO.OtherEquipment>())
        {
        }
    }
}