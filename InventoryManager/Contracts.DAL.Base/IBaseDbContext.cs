using System;
using System.Collections.Generic;

namespace Contracts.DAL.Base
{
    public interface IBaseDbContext : IBaseDbContext<Guid>
    {
    }
    
    public interface IBaseDbContext<TKey>
        where TKey : IEquatable<TKey>
    {
        Dictionary<IDomainEntityId<TKey>, IDomainEntityId<TKey>> EntityTracker { get; }
    }
}