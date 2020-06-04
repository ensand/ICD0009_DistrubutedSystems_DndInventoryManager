using AutoMapper;
using AutoMapper.Configuration;

namespace PublicApi.DTO.V1.Mappers
{
    public class ArmorMapper : BaseMapper<BLL.App.DTO.Armor, Armor>
    {
        public ArmorMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Armor, Armor>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public BLL.App.DTO.Armor MapArmorUpdateToBll(ArmorUpdate inObject)
        {
            var bllEntity = new BLL.App.DTO.Armor()
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                ArmorType = inObject.ArmorType,
                Ac = inObject.Ac,
                StealthDisadvantage = inObject.StealthDisadvantage,
                StrengthRequirement = inObject.StrengthRequirement,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
        
        public BLL.App.DTO.Armor MapNewArmorToBll(NewArmor inObject)
        {
            var bllEntity = new BLL.App.DTO.Armor()
            {
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                ArmorType = inObject.ArmorType,
                Ac = inObject.Ac,
                StealthDisadvantage = inObject.StealthDisadvantage,
                StrengthRequirement = inObject.StrengthRequirement,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
    }
}