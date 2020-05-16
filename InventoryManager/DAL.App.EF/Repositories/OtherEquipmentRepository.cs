using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class OtherEquipmentRepository : EFBaseRepository<AppDbContext, Domain.OtherEquipment, DAL.App.DTO.OtherEquipment>, IOtherEquipmentRepository
    {
        public OtherEquipmentRepository(AppDbContext dbContext) 
            : base(dbContext, new BaseDALMapper<Domain.OtherEquipment, DAL.App.DTO.OtherEquipment>())
        {
        }
    }
}