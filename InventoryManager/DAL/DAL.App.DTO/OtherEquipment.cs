using System;
using System.Text.Json.Serialization;
using Contracts.DAL.Base;
using com.enola.inventorymanager.Contracts.Domain;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class OtherEquipment : IDomainEntityId
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
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}