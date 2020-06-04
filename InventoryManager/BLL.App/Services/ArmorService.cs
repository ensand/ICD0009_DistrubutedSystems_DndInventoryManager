using BLL.App.Mappers;
using com.enola.inventorymanager.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ArmorService : BaseEntityService<IAppUnitOfWork, IArmorRepository, IArmorMapper, DAL.App.DTO.Armor, BLL.App.DTO.Armor>, IArmorService
    {
        public ArmorService(IAppUnitOfWork uow) : base(uow, uow.Armors, new ArmorMapper())
        {
        }
    }
}