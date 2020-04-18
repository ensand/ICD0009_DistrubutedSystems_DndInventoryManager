using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class MagicalItem : DomainEntity
    {
        public Guid? DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        
        [MaxLength(256)]
        public string? Spell { get; set; } = default!;

        public int MaxCharges { get; set; }
        public int CurrentCharges { get; set; }
        
        public double ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}