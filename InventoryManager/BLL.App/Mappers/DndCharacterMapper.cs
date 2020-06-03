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
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Armor, BLLAppDTO.Armor>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Weapon, BLLAppDTO.Weapon>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.MagicalItem, BLLAppDTO.MagicalItem>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OtherEquipment, BLLAppDTO.OtherEquipment>();
            
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