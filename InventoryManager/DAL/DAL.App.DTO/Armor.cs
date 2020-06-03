using System;
using System.Text.Json.Serialization;
using Contracts.DAL.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class Armor : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        
        [JsonIgnore] 
        public AppUser? AppUser { get; set; }

        public Guid DndCharacterId { get; set; }
        
        [JsonIgnore] 
        public DndCharacter? DndCharacter { get; set; }

        public string Name { get; set; } = default!;

        public string? Comment { get; set; }
        
        public string? ArmorType { get; set; }

        public string Ac { get; set; } = default!;
        
        public bool StealthDisadvantage { get; set; }
        public int? StrengthRequirement { get; set; }

        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}