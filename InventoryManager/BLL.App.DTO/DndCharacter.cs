using System;
using System.Collections.Generic;
using BLL.App.DTO.Identity;
using Contracts.DAL.Base;

namespace BLL.App.DTO
{
    public class DndCharacter : IDomainEntity
    {
        public virtual Guid Id { get; set; } = default!;
        
        public virtual string? Comment { get; set; }
        
        public virtual string Name { get; set; } = default!;
        
        public virtual Guid AppUserId { get; set; }
        public virtual AppUser<Guid>? AppUser { get; set; }
        
        public virtual float TreasureInGp { get; set; }
        
        public virtual float AllItemsValueInGp { get; set; }
        public virtual float AllItemsWeight { get; set; }
        
        public virtual int PlatinumPieces { get; set; }
        public virtual int GoldPieces { get; set; }
        public virtual int ElectrumPieces { get; set; }
        public virtual int SilverPieces { get; set; }
        public virtual int CopperPieces { get; set; }
        
        public virtual int MagicalItemCount { get; set; }
        public virtual int OtherEquipmentCount { get; set; }
        public virtual int ArmorCount { get; set; }
        public virtual int WeaponCount { get; set; }
        
        public virtual ICollection<MagicalItem>? MagicalItems { get; set; }
        public virtual ICollection<OtherEquipment>? OtherEquipment { get; set; }
        public virtual ICollection<Armor>? Armor { get; set; }
        public virtual ICollection<Weapon>? Weapons { get; set; }
    }
}