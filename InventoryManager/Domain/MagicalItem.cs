using System;
using System.ComponentModel.DataAnnotations;
using DAL.Base;
using Domain.Identity;

namespace Domain
{
    public class MagicalItem : DomainEquipmentEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        
        public Guid DndCharacterId { get; set; }
        public DndCharacter? DndCharacter { get; set; }
        
        [MaxLength(256)]
        public string? Spell { get; set; }

        public int MaxCharges { get; set; }
        public int CurrentCharges { get; set; }
    }
}