using Contracts.DAL.Base;

namespace Domain.Base
{
    public abstract class DomainEquipmentBaseEntity : IDomainEquipmentBaseEntity
    {
        public float Weight { get; set; }
        
        public float ValueInGp { get; set; }
        
        public int Quantity { get; set; }
    }
}