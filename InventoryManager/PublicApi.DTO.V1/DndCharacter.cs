using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class DndCharacter
    {
        public Guid Id { get; set; }

        [MinLength(1)]
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
        
        [MaxLength(2048)]
        public virtual string? Comment { get; set; }
        
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; }
        public int ElectrumPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }

        public int MagicalItemCount { get; set; }
        public int OtherEquipmentCount { get; set; }
        public int ArmorCount { get; set; }
        public int WeaponCount { get; set; }

        public ICollection<MagicalItem>? MagicalItems { get; set; }
        public ICollection<OtherEquipment>? OtherEquipment { get; set; }
        public ICollection<Armor>? Armor { get; set; }
        public ICollection<Weapon>? Weapons { get; set; }
        
        public float TreasureInGp => PlatinumPieces * 10 + GoldPieces + ElectrumPieces / 2 + SilverPieces / 10 + CopperPieces / 100;
        public float AllItemsValueInGp => 0;
        public float AllItemsWeight => 0;
    }
}