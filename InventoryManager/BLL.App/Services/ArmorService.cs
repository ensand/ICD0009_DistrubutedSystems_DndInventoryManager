using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ArmorService : BaseEntityService<IArmorRepository, IAppUnitOfWork, DAL.App.DTO.Armor, BLL.App.DTO.Armor>, IArmorService
    {
        public ArmorService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Armor, BLL.App.DTO.Armor>(), unitOfWork.Armors)
        {
        }
    }
}