using AutoMapper;

namespace PublicApi.DTO.V1.Mappers
{
    public class MagicalItemMapper : BaseMapper<BLL.App.DTO.MagicalItem, MagicalItem>
    {
        public MagicalItemMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.MagicalItem, MagicalItem>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLL.App.DTO.MagicalItem MapMagicalItemUpdateToBll(MagicalItemUpdate inObject)
        {
            var bllEntity = new BLL.App.DTO.MagicalItem()
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                Spell = inObject.Spell,
                MaxCharges = inObject.MaxCharges,
                CurrentCharges = inObject.CurrentCharges,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
        
        public BLL.App.DTO.MagicalItem MapNewMagicalItemToBll(NewMagicalItem inObject)
        {
            var bllEntity = new BLL.App.DTO.MagicalItem()
            {
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                Spell = inObject.Spell,
                MaxCharges = inObject.MaxCharges,
                CurrentCharges = inObject.CurrentCharges,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
    }
}