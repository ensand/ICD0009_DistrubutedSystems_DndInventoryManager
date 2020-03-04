using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class CharactersWeapons : DomainEntity
    {
        [MaxLength(36)]
        public string WeaponId { get; set; }
        public Weapon Weapon { get; set; }

        [MaxLength(36)]
        public string DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; }
    }
}