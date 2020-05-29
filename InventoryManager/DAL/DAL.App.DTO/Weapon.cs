using Domain.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class Weapon : DomainEquipmentEntity<AppUser, DAL.App.DTO.DndCharacter>
    {
        public string DamageDice { get; set; } = default!;
        
        public string DamageType { get; set; } = default!;
        
        public string WeaponType { get; set; } = default!;
       
        public string WeaponRange { get; set; } = default!;
        
        public string? Properties { get; set; }

        public bool Silvered { get; set; }
    }
}