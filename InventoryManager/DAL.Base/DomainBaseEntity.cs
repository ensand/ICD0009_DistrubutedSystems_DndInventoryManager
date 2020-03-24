using System;
using System.ComponentModel.DataAnnotations;
using Contracts.DAL.Base;

namespace DAL.Base
{
    public abstract class DomainBaseEntity : IDomainBaseEntity
    {
        public virtual Guid Id { get; set; }

        // [MaxLength(1024)]
        public virtual string? Comment { get; set; }
    }
}