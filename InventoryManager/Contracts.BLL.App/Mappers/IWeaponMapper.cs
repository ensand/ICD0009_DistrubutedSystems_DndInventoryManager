using com.enola.inventorymanager.Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IWeaponMapper : IBaseMapper<DALAppDTO.Weapon, BLLAppDTO.Weapon>
    {
        
    }
}