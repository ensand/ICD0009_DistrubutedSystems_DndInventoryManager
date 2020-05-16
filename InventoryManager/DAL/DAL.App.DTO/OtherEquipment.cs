using System;
using DAL.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class OtherEquipment : DomainEquipmentEntity
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
    }
}