using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEquipmentBaseEntity
    {
        public float Weight { get; set; }
        
        public float ValueInGp { get; set; }
        
        public int Quantity { get; set; }
    }
}