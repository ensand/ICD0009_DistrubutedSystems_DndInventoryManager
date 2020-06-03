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
            // MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            // MapperConfigurationExpression.CreateMap<DALAppDTO.Armor, BLLAppDTO.Armor>();
            // MapperConfigurationExpression.CreateMap<DALAppDTO.Weapon, BLLAppDTO.Weapon>();
            // MapperConfigurationExpression.CreateMap<DALAppDTO.MagicalItem, BLLAppDTO.MagicalItem>();
            // MapperConfigurationExpression.CreateMap<DALAppDTO.OtherEquipment, BLLAppDTO.OtherEquipment>();
            
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.DndCharacterSummary MapDndCharacterSummary(DALAppDTO.DndCharacterSummary inObject)
        {
            return Mapper.Map<BLLAppDTO.DndCharacterSummary>(inObject);
        }

        public BLLAppDTO.DndCharacter MapDndCharacter(DALAppDTO.DndCharacter inObject)
        {
            // var query = PrepareQuery(userId, noTracking);
            // var domainEntityQuery = await query
            //     .Include(e => e.Armor)
            //     .Include(e => e.Weapons)
            //     .Include(e => e.MagicalItems)
            //     .Include(e => e.OtherEquipment)
            //     .FirstOrDefaultAsync(e => e.Id.Equals(id));
            //
            // var result = MapCharacterWithEquipment(domainEntityQuery);
            //
            // return result;
            return Mapper.Map<BLLAppDTO.DndCharacter>(inObject);
        }
    }
}