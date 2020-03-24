using System;

namespace Contracts.DAL.Base
{
    public interface IDomainEntityMetadata
    {
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}