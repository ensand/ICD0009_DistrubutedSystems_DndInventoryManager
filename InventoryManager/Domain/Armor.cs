using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class Armor : DomainEntity
    {
        public Guid DndCharacterId { get; set; }
        public DndCharacter DndCharacter { get; set; } = default!;
        
        public bool BaseItem { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Type { get; set; } = default!;

        public int BaseAc { get; set; }
        public int Weight { get; set; }
        public int ValueInGp { get; set; }
        public int Quantity { get; set; }
        public bool StealthDisadvantage { get; set; }
    }
}