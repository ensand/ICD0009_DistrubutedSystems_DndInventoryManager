using System;
using System.Text.Json.Serialization;
using Domain.Base;
using Domain.Identity;

namespace Domain
{
    public class OtherEquipment : DomainEquipmentEntity
    {
        public Guid AppUserId { get; set; }
        
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
        
        public Guid DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; }
    }
}