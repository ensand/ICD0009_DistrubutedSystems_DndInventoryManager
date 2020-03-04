using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class MagicalItem : DomainEntity
    {
        public ICollection<CharactersMagicalItems> Characters { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Spell { get; set; }

        public int MaxCharges { get; set; }
        public int CurrentCharges { get; set; }
        public int Quantity { get; set; }
    }
}