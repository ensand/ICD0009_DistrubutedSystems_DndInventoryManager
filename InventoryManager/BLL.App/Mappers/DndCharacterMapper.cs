using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class DndCharacterMapper : BaseMapper<DALAppDTO.DndCharacter, BLLAppDTO.DndCharacter>, IDndCharacterMapper
    {
        public DndCharacterMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.DndCharacter, BLLAppDTO.DndCharacter>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.DndCharacterSummary, BLLAppDTO.DndCharacterSummary>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.DndCharacterSummary MapDndCharacterSummary(DALAppDTO.DndCharacterSummary inObject)
        {
            return Mapper.Map<BLLAppDTO.DndCharacterSummary>(inObject);
        }

        public BLLAppDTO.DndCharacter MapDndCharacter(DALAppDTO.DndCharacter inObject)
        {
            return Mapper.Map<BLLAppDTO.DndCharacter>(inObject);
        }
    }
}