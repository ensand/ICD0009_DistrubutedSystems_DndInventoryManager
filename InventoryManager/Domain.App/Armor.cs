using System.ComponentModel.DataAnnotations;
using com.enola.inventorymanager.Domain.Base;
using Domain.Identity;

namespace Domain.App
{
    public class Armor : DomainEquipmentEntity<AppUser, DndCharacter>
    {
        [MaxLength(128)]
        public string? ArmorType { get; set; } // light/medium/heavy

        [MinLength(1)] 
        [MaxLength(128)] 
        public string Ac { get; set; } = default!; // base + modifiers
        
        public bool StealthDisadvantage { get; set; }
        public int? StrengthRequirement { get; set; }
    }
}