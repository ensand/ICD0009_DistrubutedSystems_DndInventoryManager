using System;
using Domain.Base;
using Domain.Identity;

namespace DAL.App.DTO
{
    public class OtherEquipment : DomainEquipmentEntity<AppUser, DAL.App.DTO.DndCharacter>
    {
    }

    public class OtherEquipmentSummary
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Comment { get; set; }
        public float ValueInGp { get; set; }
        public float Weight { get; set; }
        public int Quantity { get; set; }
    }
}