using System;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseService
    {
    }

    public interface IBaseEntityService<TBLLEntity> : IBaseService, IBaseRepository<Guid, TBLLEntity>
        where TBLLEntity : class, IDomainEntity<Guid>, new()
    {
    }
}