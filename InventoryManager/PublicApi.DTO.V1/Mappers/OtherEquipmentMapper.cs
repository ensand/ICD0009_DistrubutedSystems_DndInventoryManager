using AutoMapper;

namespace PublicApi.DTO.V1.Mappers
{
    public class OtherEquipmentMapper : BaseMapper<BLL.App.DTO.OtherEquipment, OtherEquipment>
    {
        public OtherEquipmentMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.OtherEquipment, OtherEquipment>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLL.App.DTO.OtherEquipment MapOtherEquipmentUpdateToBll(OtherEquipmentUpdate inObject)
        {
            var bllEntity = new BLL.App.DTO.OtherEquipment()
            {
                Id = inObject.Id,
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
        
        public BLL.App.DTO.OtherEquipment MapNewOtherEquipmentToBll(NewOtherEquipment inObject)
        {
            var bllEntity = new BLL.App.DTO.OtherEquipment()
            {
                AppUserId = inObject.AppUserId,
                DndCharacterId = inObject.DndCharacterId,
                Name = inObject.Name,
                Comment = inObject.Comment,
                Weight = inObject.Weight,
                ValueInGp = inObject.ValueInGp,
                Quantity = inObject.Quantity
            };

            return bllEntity;
        }
    }
}