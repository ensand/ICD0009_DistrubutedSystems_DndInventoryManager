using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class OtherEquipmentDto
    {
        public Guid Id { get; set; }

        [MinLength(1)] 
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
}