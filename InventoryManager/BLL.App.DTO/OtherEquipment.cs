using System;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class OtherEquipment : IDomainEquipmentEntity
    {
        public virtual Guid Id { get; set; } = default!;
        
        public virtual string? Comment { get; set; }

        public virtual string Name { get; set; } = default!;
        
        public virtual float Weight { get; set; }
        
        public virtual float ValueInGp { get; set; }
        
        public virtual int Quantity { get; set; }
        
        public virtual Guid AppUserId { get; set; }
        public virtual AppUser<Guid>? AppUser { get; set; }
        
        public virtual Guid DndCharacterId { get; set; }
        public virtual DndCharacter? DndCharacter { get; set; }
    }
}