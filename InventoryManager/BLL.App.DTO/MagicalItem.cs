using System;
using BLL.App.DTO.Identity;
using DAL.Base;

namespace BLL.App.DTO
{
    public class MagicalItem : DomainEquipmentEntity
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
        
        public string? Spell { get; set; }

        public int MaxCharges { get; set; }
        public int CurrentCharges { get; set; }
    }
}