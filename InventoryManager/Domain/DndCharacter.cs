using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class DndCharacter : DomainEntity
    {
        public ICollection<CharactersMagicalItems> MagicalItems { get; set; }
        public ICollection<CharactersEquipment> Equipment { get; set; }
        public ICollection<CharactersArmor> Armor { get; set; }
        public ICollection<CharactersWeapons> Weapons { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        public int? Level { get; set; }

        public int GoldPieces { get; set; } = default!;
        public int PlatinumPieces { get; set; } = default!;
        public int SilverPieces { get; set; } = default!;
        public int CopperPieces { get; set; } = default!;

        public double TotalTreasureInGp => GoldPieces + PlatinumPieces / 5 + SilverPieces / 10 + CopperPieces / 100;
    }
}