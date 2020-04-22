using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Armor : DomainEquipmentEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; }
        
        [MaxLength(128)]
        public string? ArmorType { get; set; } // light/medium/heavy

        [MinLength(1)] 
        [MaxLength(128)] 
        public string Ac { get; set; } = default!; // base + modifiers
        
        public bool StealthDisadvantage { get; set; }
        public int? StrengthRequirement { get; set; }
    }
}