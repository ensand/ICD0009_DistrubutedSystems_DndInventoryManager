using System;
using com.enola.inventorymanager.Contracts.Domain;

namespace com.enola.inventorymanager.Contracts.DAL.Base
{
    public interface IBaseEntityTracker : IBaseEntityTracker<Guid>
    {
    }
    
    public interface IBaseEntityTracker<TKey>
        where TKey : IEquatable<TKey>
    {
        void AddToEntityTracker(IDomainEntityId<TKey> internalEntity, IDomainEntityId<TKey> externalEntity);
    }
}