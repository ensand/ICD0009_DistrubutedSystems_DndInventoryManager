using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class MagicalItem
    {
        public Guid Id { get; set; }

        [MinLength(1)]
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
        
        [MaxLength(2048)]
        public virtual string? Comment { get; set; }

        [MaxLength(256)]
        public string? Spell { get; set; }

        public int MaxCharges { get; set; }
        public int CurrentCharges { get; set; }
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}