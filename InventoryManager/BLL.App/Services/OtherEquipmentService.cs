using BLL.App.Mappers;
using com.enola.inventorymanager.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class OtherEquipmentService : BaseEntityService<IAppUnitOfWork, IOtherEquipmentRepository, IOtherEquipmentMapper, DAL.App.DTO.OtherEquipment, BLL.App.DTO.OtherEquipment>, IOtherEquipmentService
    {
        public OtherEquipmentService(IAppUnitOfWork uow) : base(uow, uow.OtherEquipments, new OtherEquipmentMapper())
        {
        }
    }
}