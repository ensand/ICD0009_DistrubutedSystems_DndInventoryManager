using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.Identity;

namespace Domain.App
{
    public class MagicalItem : DomainEquipmentEntity<AppUser, DndCharacter>
    {
        [MaxLength(256)]
        public string? Spell { get; set; }

        public int MaxCharges { get; set; }
        public int CurrentCharges { get; set; }
    }
}