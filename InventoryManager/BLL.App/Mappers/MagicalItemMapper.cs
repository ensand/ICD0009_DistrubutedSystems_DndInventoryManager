using com.enola.inventorymanager.BLL.Base.Mappers;

using Contracts.BLL.App.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class MagicalItemMapper : BaseMapper<DALAppDTO.MagicalItem, BLLAppDTO.MagicalItem>, IMagicalItemMapper
    {
        
    }
}