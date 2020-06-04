using BLL.App.Mappers;
using com.enola.inventorymanager.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class MagicalItemService : BaseEntityService<IAppUnitOfWork, IMagicalItemRepository, IMagicalItemMapper, DAL.App.DTO.MagicalItem, BLL.App.DTO.MagicalItem>, IMagicalItemService
    {
        public MagicalItemService(IAppUnitOfWork uow) : base(uow, uow.MagicalItems, new MagicalItemMapper())
        {
        }
    }
}