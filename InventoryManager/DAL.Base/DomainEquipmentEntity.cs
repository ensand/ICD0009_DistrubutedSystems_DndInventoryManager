using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEquipmentEntity : DomainEquipmentEntity<Guid>
    {
    }
    
    public abstract class DomainEquipmentEntity<TKey> : IDomainEquipmentEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
        
        [MaxLength(2048)]
        public virtual string? Comment { get; set; }

        [MinLength(1)]
        [MaxLength(512)]
        public virtual string Name { get; set; } = default!;
        
        public virtual float Weight { get; set; }
        
        public virtual float ValueInGp { get; set; }
        
        public virtual int Quantity { get; set; }
    }
}