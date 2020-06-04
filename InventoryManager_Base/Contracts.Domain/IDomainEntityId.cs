using System;

namespace com.enola.inventorymanager.Contracts.Domain
{
    public interface IDomainEntityId : IDomainEntityId<Guid>
    {
    }
    
    public interface IDomainEntityId<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }
    }
}