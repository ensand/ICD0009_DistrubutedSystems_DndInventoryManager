﻿
namespace Contracts.DAL.Base
{
    public interface IDomainEntity
    {
        public string? Comment { get; set; }
        public string Name { get; set; }
    }
}