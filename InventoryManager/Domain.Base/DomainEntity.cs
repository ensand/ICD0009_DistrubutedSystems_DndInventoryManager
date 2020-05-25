using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace Domain.Base
{
    public abstract class DomainEntity : DomainEntity<Guid>
    {
    }

    
    public abstract class DomainEntity<TKey> : IDomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
        
        [MaxLength(2048)]
        public virtual string? Comment { get; set; }

        [MinLength(1)]
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
    }
}