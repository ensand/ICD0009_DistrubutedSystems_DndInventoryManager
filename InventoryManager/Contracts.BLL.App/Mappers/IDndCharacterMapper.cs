using Contracts.BLL.Base.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Mappers
{
    public interface IDndCharacterMapper : IBaseMapper<DALAppDTO.DndCharacter, BLLAppDTO.DndCharacter>
    {
        BLLAppDTO.DndCharacterSummary MapDndCharacterSummary(DALAppDTO.DndCharacterSummary inObject);
        BLLAppDTO.DndCharacter MapDndCharacter(DALAppDTO.DndCharacter inObject);
    }
}