using System;
using System.Collections.Generic;
using DAL.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class DndCharacter : DomainEntity
    {
        public Guid Id { get; set; } = default!;
        
        public virtual string? Comment { get; set; }

        public virtual string Name { get; set; } = default!;

        public Guid AppUserId { get; set; }
        public AppUser<Guid>? AppUser { get; set; }
        
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; }
        public int ElectrumPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }

        public ICollection<MagicalItem>? MagicalItems { get; set; }
        public ICollection<OtherEquipment>? OtherEquipment { get; set; }
        public ICollection<Armor>? Armor { get; set; }
        public ICollection<Weapon>? Weapons { get; set; }
    }
}