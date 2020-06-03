using AutoMapper;

namespace PublicApi.DTO.V1.Mappers
{
    public class DndCharacterMapper : BaseMapper<BLL.App.DTO.DndCharacter, DndCharacter>
    {
        public DndCharacterMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.DndCharacter, DndCharacter>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.DndCharacterSummary, DndCharacterSummary>();
            
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Armor, Armor>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Weapon, Weapon>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.MagicalItem, MagicalItem>();
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OtherEquipment, OtherEquipment>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public DndCharacterSummary MapDndCharacterSummary(BLL.App.DTO.DndCharacterSummary inObject)
        {
            return Mapper.Map<DndCharacterSummary>(inObject);
        }

        public DndCharacter MapDndCharacter(BLL.App.DTO.DndCharacter inObject)
        {
            return Mapper.Map<DndCharacter>(inObject);
        }

        public BLL.App.DTO.DndCharacter MapDndCharacterUpdateToBll(DndCharacterUpdate inObject)
        {
            var bllEntity = new BLL.App.DTO.DndCharacter()
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                PlatinumPieces = inObject.PlatinumPieces,
                GoldPieces = inObject.GoldPieces,
                ElectrumPieces = inObject.ElectrumPieces,
                SilverPieces = inObject.SilverPieces,
                CopperPieces = inObject.CopperPieces
            };
            
            return bllEntity;
        }
        
        public BLL.App.DTO.DndCharacter MapNewDndCharacterToBll(NewDndCharacter inObject)
        {
            var bllEntity = new BLL.App.DTO.DndCharacter()
            {
                AppUserId = inObject.AppUserId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                PlatinumPieces = inObject.PlatinumPieces,
                GoldPieces = inObject.GoldPieces,
                ElectrumPieces = inObject.ElectrumPieces,
                SilverPieces = inObject.SilverPieces,
                CopperPieces = inObject.CopperPieces
            };
            
            return bllEntity;
        }
    }
}