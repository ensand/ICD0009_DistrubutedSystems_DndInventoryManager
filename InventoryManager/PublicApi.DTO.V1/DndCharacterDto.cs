using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class DndCharacterDto
    {
        public Guid Id { get; set; }

        [MinLength(1)]
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
        
        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; }
        public int ElectrumPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }

        public float TreasureInGp => PlatinumPieces * 10 + GoldPieces + ElectrumPieces / 2 + SilverPieces / 10 + CopperPieces / 100;
        // public float TotalTreasureInGp => moneys + equipment values
        // public float EquipmentWeight => equipment weight
        // public float TotalEquipmentWeight => moneys weight + equipment weight
        
        public int MagicalItemsCount { get; set; }
        public int OtherEquipmentCount { get; set; }
        public int ArmorCount { get; set; }
        public int WeaponsCount { get; set; }
    }
}