using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEquipmentBaseEntity : IDomainEquipmentBaseEntity
    {
        public float Weight { get; set; }
        
        public float ValueInGp { get; set; }
        
        public int Quantity { get; set; }
    }
}