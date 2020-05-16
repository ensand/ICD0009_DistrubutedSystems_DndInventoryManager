using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class OtherEquipment
    {
        public Guid Id { get; set; }

        [MinLength(1)] 
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
        
        [MaxLength(2048)]
        public virtual string? Comment { get; set; }
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}