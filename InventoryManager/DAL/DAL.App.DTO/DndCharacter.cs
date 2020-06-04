using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using com.enola.inventorymanager.Contracts.Domain;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO
{
    public class DndCharacter : IDomainEntityId
    {
        public Guid Id { get; set; }

        public Guid AppUserId { get; set; }
        
        [JsonIgnore] 
        public AppUser? AppUser { get; set; }
        
        public string Name { get; set; } = default!;

        public string? Comment { get; set; }
        
        public float TreasureInGp { get; set; }
        
        public float AllItemsValueInGp { get; set; }
        public float AllItemsWeight { get; set; }
        
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