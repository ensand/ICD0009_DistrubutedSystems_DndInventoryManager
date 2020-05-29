using Domain.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class MagicalItem : DomainEquipmentEntity<AppUser, DAL.App.DTO.DndCharacter>
    {
        public string? Spell { get; set; }

        public int MaxCharges { get; set; }
        public int CurrentCharges { get; set; }
    }
}