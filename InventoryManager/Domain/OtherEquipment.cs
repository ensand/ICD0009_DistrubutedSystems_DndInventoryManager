using System;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class OtherEquipment : DomainEquipmentEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; }
    }
}