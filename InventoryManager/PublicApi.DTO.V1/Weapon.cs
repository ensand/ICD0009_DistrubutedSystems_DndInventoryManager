using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class Weapon
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; } = default!;
        
        public string? Comment { get; set; }
        
        public string DamageDice { get; set; } = default!; // 1d20 + mods
        
        public string DamageType { get; set; } = default!; // piercing/slashing/force/fire/thunder/bludgeoning/poison 
        
        public string WeaponType { get; set; } = default!; // simple/martial
        
        public string WeaponRange { get; set; } = default!; // melee/ranged
        
        public string? Properties { get; set; }

        public bool Silvered { get; set; }
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }

    public class NewWeapon
    {
        public Guid DndCharacterId { get; set; }
        public Guid AppUserId { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(512)] 
        public string Name { get; set; } = default!;
        
        [MaxLength(2048)]
        public string? Comment { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(128)]
        public string DamageDice { get; set; } = default!;
        
        [Required]
        [MinLength(1)] 
        [MaxLength(128)] 
        public string DamageType { get; set; } = default!;
        
        [Required]
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponType { get; set; } = default!;
        
        [Required]
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponRange { get; set; } = default!;
        
        [MaxLength(1024)] 
        public string? Properties { get; set; }

        public bool Silvered { get; set; }
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
    
    public class WeaponUpdate : NewWeapon
    {
        public Guid Id { get; set; }
    }
}