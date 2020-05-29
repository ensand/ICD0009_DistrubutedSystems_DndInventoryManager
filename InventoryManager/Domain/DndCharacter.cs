using System.Collections.Generic;
using Domain.Base;
using Domain.Identity;

namespace Domain
{
    public class DndCharacter : DomainEntity<AppUser>
    {
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