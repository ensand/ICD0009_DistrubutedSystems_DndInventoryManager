using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using com.enola.inventorymanager.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace com.enola.inventorymanager.Domain.Base
{
    public abstract class DomainEntity<TUser> : DomainEntity<Guid, TUser>
        where TUser : IdentityUser<Guid>
    {
    }

    
    public abstract class DomainEntity<TKey, TUser> : IDomainEntityUser<TKey, TUser>, IDomainEntityId<TKey>, IDomainEntity
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
    {
        public virtual TKey AppUserId { get; set; } = default!;
        
        [JsonIgnore]
        public virtual TUser? AppUser { get; set; }
        
        public virtual TKey Id { get; set; } = default!;
        
        [MaxLength(2048)]
        public virtual string? Comment { get; set; }

        [MinLength(1)]
        [MaxLength(512)] 
        public virtual string Name { get; set; } = default!;
    }
}