using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using com.enola.inventorymanager.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.Base
{
    public class DomainEquipmentEntity<TUser, TOwnerEntity> : DomainEquipmentEntity<Guid, TUser, TOwnerEntity>
        where TUser : IdentityUser<Guid>
        where TOwnerEntity : class, IDomainEntityUser<Guid, TUser>
    {
    }
    
    public class DomainEquipmentEntity<TKey, TUser, TOwnerEntity> : 
        IDomainEntityUser<TKey, TUser>,
        IDomainEntityId<TKey>, 
        IDomainEntity, 
        IDomainEquipmentEntity<TKey, TUser, TOwnerEntity>
    
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
        where TOwnerEntity : class, IDomainEntityUser<TKey, TUser>
    {
        public virtual TKey AppUserId { get; set; } = default!;
        
        [JsonIgnore]
        public virtual TUser? AppUser { get; set; }
        
        public virtual TKey DndCharacterId { get; set; } = default!;
        
        [JsonIgnore]
        public virtual TOwnerEntity? DndCharacter { get; set; }
        
        public virtual TKey Id { get; set; } = default!;
        
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