using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Weapon : DomainEntity
    {
        public Guid? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public bool BaseItem { get; set; }

        public Guid? DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(128)]
        public string DamageDice { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string DamageType { get; set; } = default!; // Bludgeoning, piercing, slashing
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponType { get; set; } = default!; // simple/martial
       
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponRange { get; set; } = default!; //  melee/ranged
        
        [MinLength(1)] 
        [MaxLength(512)] 
        public string Properties { get; set; } = default!;

        public bool Silvered { get; set; }

        public bool? Proficiency { get; set; }

        public double ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}