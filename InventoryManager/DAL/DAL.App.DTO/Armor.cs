using Domain.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class Armor : DomainEquipmentEntity<AppUser, DAL.App.DTO.DndCharacter>
    {
        public string? ArmorType { get; set; }

        public string Ac { get; set; } = default!;
        
        public bool StealthDisadvantage { get; set; }
        public int? StrengthRequirement { get; set; }
    }
}