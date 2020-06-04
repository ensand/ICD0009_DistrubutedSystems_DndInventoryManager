using System;
using Microsoft.AspNetCore.Identity;

namespace Contracts.Domain
{
    public interface IDomainEquipmentEntity<TUser, TOwnerEntity> : IDomainEquipmentEntity<Guid, TUser, TOwnerEntity>
        where TUser : IdentityUser<Guid>
        where TOwnerEntity : class, IDomainEntityUser<Guid, TUser>
    {
    }
    
    public interface IDomainEquipmentEntity<TKey, TUser, TOwnerEntity>
        where TKey : IEquatable<TKey>
        where TUser : IdentityUser<TKey>
        where TOwnerEntity : class, IDomainEntityUser<TKey, TUser>
    {
        public TKey DndCharacterId { get; set; }
        public TOwnerEntity DndCharacter { get; set; }
        
        public float Weight { get; set; }
        
        public float ValueInGp { get; set; }
        
        public int Quantity { get; set; }
    }
}