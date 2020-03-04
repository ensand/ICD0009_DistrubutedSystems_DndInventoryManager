using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class CharactersMagicalItems : DomainEntity
    {
        [MaxLength(36)]
        public string MagicalItemId { get; set; }
        public MagicalItem MagicalItem { get; set; }

        [MaxLength(36)]
        public string DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; }
    }
}