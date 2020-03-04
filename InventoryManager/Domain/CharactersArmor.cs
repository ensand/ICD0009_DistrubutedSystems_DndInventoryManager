using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class CharactersArmor : DomainEntity
    {
        [MaxLength(36)]
        public string ArmorId { get; set; }
        public Armor Armor { get; set; }

        [MaxLength(36)]
        public string DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; }
    }
}