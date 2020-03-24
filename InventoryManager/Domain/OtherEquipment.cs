using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class OtherEquipment : DomainEntity
    {
        public Guid DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; } = default!;
        
        public bool BaseItem { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;

        public int ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}