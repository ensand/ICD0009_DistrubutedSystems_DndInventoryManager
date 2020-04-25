using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class WeaponDto
    {
        public Guid Id { get; set; }

        [MinLength(1)] 
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(128)]
        public string DamageDice { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string DamageType { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponType { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(128)] 
        public string WeaponRange { get; set; } = default!;
        
        [MaxLength(1024)] 
        public string? Properties { get; set; }

        public bool Silvered { get; set; }
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}