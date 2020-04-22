using System;
using DAL.Base;

namespace Domain
{
    public class OtherEquipment : DomainEquipmentEntity
    {
        public Guid DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; }
    }
}