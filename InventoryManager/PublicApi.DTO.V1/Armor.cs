using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class Armor
    {
        public Guid Id { get; set; }

        public Guid DndCharacterId { get; set; }
        
        [MinLength(1)]
        [MaxLength(512)] 
        public string Name { get; set; } = default!;
        
        [MaxLength(128)]
        public string? ArmorType { get; set; } // light/medium/heavy

        [MinLength(1)] 
        [MaxLength(128)] 
        public string Ac { get; set; } = default!; // base + modifiers
        
        public bool StealthDisadvantage { get; set; }
        public int? StrengthRequirement { get; set; }

        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}