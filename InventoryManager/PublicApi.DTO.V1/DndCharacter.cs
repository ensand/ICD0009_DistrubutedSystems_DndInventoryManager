using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class DndCharacter
    {
        public Guid Id { get; set; }
        
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

        public string Name { get; set; } = default!;
        public string? Comment { get; set; }
        public float TreasureInGp { get; set; }

        public int MagicalItemCount { get; set; }
        public int OtherEquipmentCount { get; set; }
        public int ArmorCount { get; set; }
        public int WeaponCount { get; set; }
    }

    public class NewDndCharacter
    {
        public Guid AppUserId { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(512)] 
        public string Name { get; set; } = default!;
        
        [MaxLength(2048)]
        public string? Comment { get; set; }

        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; }
        public int ElectrumPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }
    }
    
    public class DndCharacterUpdate : NewDndCharacter
    {
        public Guid Id { get; set; }
    }
}