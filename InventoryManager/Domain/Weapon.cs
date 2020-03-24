using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Weapon : DomainEntity
    {
        public Guid DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; } = default!;
        
        public bool BaseItem { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        [MinLength(1)] 
        [MaxLength(128)] 
        public string AttackType { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponType { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponSize { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(128)]
        public string ToHit { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Damage { get; set; } = default!;

        public int Range { get; set; }
        public int ValueInGp { get; set; }
        public int Quantity { get; set; }

        public bool Silvered { get; set; }
        public bool Finesse { get; set; }
        public bool TwoHanded { get; set; }
    }
}