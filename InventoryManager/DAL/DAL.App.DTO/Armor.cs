using System;
using DAL.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class Armor : DomainEquipmentEntity
    {
        public Guid Id { get; set; } = default!;
        
        public virtual string? Comment { get; set; }

        public virtual string Name { get; set; } = default!;
        
        public virtual float Weight { get; set; }
        
        public virtual float ValueInGp { get; set; }
        
        public virtual int Quantity { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser<Guid>? AppUser { get; set; }
        
        public Guid DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; }
        
        public string? ArmorType { get; set; }

        public string Ac { get; set; } = default!;
        
        public bool StealthDisadvantage { get; set; }
        public int? StrengthRequirement { get; set; }
    }
}