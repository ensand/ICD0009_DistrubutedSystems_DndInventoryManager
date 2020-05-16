using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OtherEquipmentService : BaseEntityService<IOtherEquipmentRepository, IAppUnitOfWork, DAL.App.DTO.OtherEquipment, BLL.App.DTO.OtherEquipment>, IOtherEquipmentService
    {
        public OtherEquipmentService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.OtherEquipment, BLL.App.DTO.OtherEquipment>(), unitOfWork.OtherEquipments)
        {
        }
    }
}