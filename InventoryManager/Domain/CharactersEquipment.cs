using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class CharactersEquipment : DomainEntity
    {
        [MaxLength(36)]
        public string EquipmentId { get; set; }
        public OtherEquipment Equipment { get; set; }

        [MaxLength(36)]
        public string DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; }
    }
}