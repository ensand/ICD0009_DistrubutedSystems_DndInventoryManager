using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class MagicalItemService : BaseEntityService<IMagicalItemRepository, IAppUnitOfWork, DAL.App.DTO.MagicalItem, BLL.App.DTO.MagicalItem>, IMagicalItemService
    {
        public MagicalItemService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.MagicalItem, BLL.App.DTO.MagicalItem>(), unitOfWork.MagicalItems)
        {
        }
    }
}