using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;

namespace DAL.App.EF.Repositories
{
    public class OtherEquipmentRepository : 
        EFBaseRepository<AppEntityTracker, Domain.Identity.AppUser, Domain.OtherEquipment, DAL.App.DTO.OtherEquipment>, 
        IOtherEquipmentRepository
    {
        public OtherEquipmentRepository(AppEntityTracker repoEntityTracker) 
            : base(repoEntityTracker, new DAL.Base.Mappers.BaseMapper<Domain.OtherEquipment, DAL.App.DTO.OtherEquipment>())
        {
        }
    }
}