using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEquipmentEntity : IDomainEquipmentEntity<Guid>
    {
    }
    
    public interface IDomainEquipmentEntity<TKey> : IDomainEntity<TKey>, IDomainEquipmentBaseEntity
        where TKey : struct, IEquatable<TKey>
    {
    }
}