using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class DndCharacter : DomainEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        public int? Level { get; set; }

        public int PlatinumPieces { get; set; }
        public int GoldPieces { get; set; }
        public int ElectrumPieces { get; set; }
        public int SilverPieces { get; set; }
        public int CopperPieces { get; set; }

        public double TotalTreasureInGp => PlatinumPieces * 10 + GoldPieces + ElectrumPieces / 2 + SilverPieces / 10 + CopperPieces / 100;
        
        public ICollection<MagicalItem>? MagicalItems { get; set; }
        public ICollection<OtherEquipment>? OtherEquipment { get; set; }
        public ICollection<Armor>? Armor { get; set; }
        public ICollection<Weapon>? Weapons { get; set; }
    }
}