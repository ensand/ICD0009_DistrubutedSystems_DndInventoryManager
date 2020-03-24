using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainEntity : IDomainEntity
    {
        public virtual Guid Id { get; set; }
        
        [MaxLength(1024)]
        public virtual string? Comment { get; set; }
    }
}