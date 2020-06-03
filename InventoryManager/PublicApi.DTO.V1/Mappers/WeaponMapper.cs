using AutoMapper;

namespace PublicApi.DTO.V1.Mappers
{
    public class WeaponMapper : BaseMapper<BLL.App.DTO.Weapon, Weapon>
    {
        public WeaponMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.Weapon, Weapon>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLL.App.DTO.Weapon MapWeaponUpdateToBll(WeaponUpdate inObject)
        {
            var bllEntity = new BLL.App.DTO.Weapon()
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                DamageDice = inObject.DamageDice,
                DamageType = inObject.DamageType,
                WeaponType = inObject.WeaponType,
                WeaponRange = inObject.WeaponRange,
                Properties = inObject.Properties,
                Silvered = inObject.Silvered,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
        
        public BLL.App.DTO.Weapon MapNewWeaponToBll(NewWeapon inObject)
        {
            var bllEntity = new BLL.App.DTO.Weapon()
            {
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                DamageDice = inObject.DamageDice,
                DamageType = inObject.DamageType,
                WeaponType = inObject.WeaponType,
                WeaponRange = inObject.WeaponRange,
                Properties = inObject.Properties,
                Silvered = inObject.Silvered,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
    }
}