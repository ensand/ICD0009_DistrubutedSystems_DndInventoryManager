using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.Identity;

namespace Domain.App
{
    public class Weapon : DomainEquipmentEntity<AppUser, DndCharacter>
    {
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
        
        [MaxLength(1024)] 
        public string? Properties { get; set; }
    }
}