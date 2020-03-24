using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity : IDomainEntity<Guid>
    {
    }
    
    // No metadata needed initially
    // public interface IDomainEntity<TKey> : IDomainBaseEntity<TKey>, IDomainEntityMetadata
    public interface IDomainEntity<TKey> : IDomainBaseEntity<TKey>
        where TKey : struct, IComparable
    {
    }
}