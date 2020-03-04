using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class Armor : DomainEntity
    {
        public ICollection<CharactersArmor> Characters { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Type { get; set; }

        public int BaseAc { get; set; }
        public int Weight { get; set; }
        public int ValueInGp { get; set; }
        public int Quantity { get; set; }
        public bool StealthDisadvantage { get; set; }
    }
}