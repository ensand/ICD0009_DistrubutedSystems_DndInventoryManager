using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntity : IDomainEntity<Guid>
    {
    }
    
    public interface IDomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public string? Comment { get; set; }

        public string Name { get; set; }
    }
}