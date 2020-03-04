using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Weapon : DomainEntity
    {
        public ICollection<CharactersWeapons> Characters { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; }

        [MinLength(1)] 
        [MaxLength(128)] 
        public string AttackType { get; set; }
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponType { get; set; }
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponSize { get; set; }

        [MinLength(1)]
        [MaxLength(128)]
        public string ToHit { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Damage { get; set; }

        public int Range { get; set; }
        public int ValueInGp { get; set; }
        public int Quantity { get; set; }

        public bool Silvered { get; set; }
        public bool Finesse { get; set; }
        public bool TwoHanded { get; set; }
    }
}