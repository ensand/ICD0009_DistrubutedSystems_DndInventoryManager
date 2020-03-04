using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DAL.Base;

namespace Domain
{
    public class OtherEquipment : DomainEntity
    {
        public ICollection<CharactersEquipment> Characters { get; set; }
        
        [MinLength(1)]
        [MaxLength(128)]
        public string Name { get; set; }

        public int ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}