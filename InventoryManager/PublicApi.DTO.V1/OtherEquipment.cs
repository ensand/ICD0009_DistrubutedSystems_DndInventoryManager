using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO.V1
{
    public class OtherEquipment
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        
        public string? Comment { get; set; }
        
        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }
    
    public class NewOtherEquipment
    {
        public Guid DndCharacterId { get; set; }
        public Guid AppUserId { get; set; }
        
        [Required]
        [MinLength(1)]
        [MaxLength(512)] 
        public string Name { get; set; } = default!;
        
        [MaxLength(2048)]
        public string? Comment { get; set; }

        public float Weight { get; set; }
        public float ValueInGp { get; set; }
        public int Quantity { get; set; }
    }

    public class OtherEquipmentUpdate : NewOtherEquipment
    {
        public Guid Id { get; set; }
    }
}