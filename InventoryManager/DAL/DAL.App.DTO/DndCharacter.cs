using System;
using System.Collections.Generic;
using Domain.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class DndCharacter : DomainEntity<AppUser>
    {
        public float TreasureInGp { get; set; }
        
        public float AllItemsValueInGp { get; set; }
        public float AllItemsWeight { get; set; }
        
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; }
        public int ElectrumPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }
        
        public ICollection<MagicalItem>? MagicalItems { get; set; }
        public ICollection<OtherEquipmentSummary>? OtherEquipment { get; set; }
        public ICollection<Armor>? Armor { get; set; }
        public ICollection<Weapon>? Weapons { get; set; }
    }

    public class DndCharacterSummary
    {
        public Guid Id { get; set; } = default!;

        public string? Comment { get; set; }
        public string Name { get; set; } = default!;
        public float TreasureInGp { get; set; }

        public int MagicalItemCount { get; set; }
        public int OtherEquipmentCount { get; set; }
        public int ArmorCount { get; set; }
        public int WeaponCount { get; set; }
    }
}