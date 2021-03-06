using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using com.enola.inventorymanager.Contracts.BLL.Base.Mappers;
using com.enola.inventorymanager.Contracts.BLL.Base.Services;
using com.enola.inventorymanager.Contracts.DAL.Base;
using com.enola.inventorymanager.Contracts.DAL.Base.Repositories;
using com.enola.inventorymanager.Contracts.Domain;

namespace com.enola.inventorymanager.BLL.Base.Services
{
    public class BaseEntityService<TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity> : 
        BaseEntityService<Guid, TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity>, 
        IBaseEntityService<TBLLEntity>
    
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
        where TRepository : IBaseRepository<Guid, TDALEntity>
        where TMapper : IBaseMapper<TDALEntity, TBLLEntity>
        where TDALEntity : class, IDomainEntityId<Guid>, new()
        where TBLLEntity : class, IDomainEntityId<Guid>, new()
    {
        // ReSharper disable once MemberCanBeProtected.Global
        public BaseEntityService(TUnitOfWork uow, TRepository repository, TMapper mapper) : base(uow, repository, mapper)
        {
        }

    }
    
    public class BaseEntityService<TKey, TUnitOfWork, TRepository, TMapper, TDALEntity, TBLLEntity> : IBaseEntityService<TKey, TBLLEntity>
        where TKey : IEquatable<TKey>
        where TUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker<TKey>
        where TRepository : IBaseRepository<TKey, TDALEntity>
        where TMapper : IBaseMapper<TDALEntity, TBLLEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TBLLEntity : class, IDomainEntityId<TKey>, new()

    {
        // ReSharper disable MemberCanBePrivate.Global
        protected readonly TUnitOfWork UnitOfWork;
        protected readonly TRepository Repository;
        protected readonly TMapper Mapper;
        // ReSharper enable MemberCanBePrivate.Global

        // ReSharper disable once MemberCanBeProtected.Global
        public BaseEntityService(TUnitOfWork uow, TRepository repository, TMapper mapper)
        {
            UnitOfWork = uow;
            Repository = repository;
            Mapper = mapper;
        }

        
        public virtual async Task<IEnumerable<TBLLEntity>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var dalEntities = await Repository.GetAllAsync(userId, noTracking);
            var result = dalEntities.Select(e => Mapper.Map(e));
            
            return result;
        }

        public virtual async Task<TBLLEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true)
        {
            var dalEntity = await Repository.FirstOrDefaultAsync(id, userId, noTracking);
            var result = Mapper.Map(dalEntity);
            
            return result;
        }

        public TBLLEntity Add(TBLLEntity entity)
        {
            var dalEntity = Mapper.Map(entity);
            var trackedDALEntity = Repository.Add(dalEntity);
            UnitOfWork.AddToEntityTracker(trackedDALEntity, entity);
            var result = Mapper.Map(trackedDALEntity);
            
            return result;
        }

        public virtual async Task<TBLLEntity> UpdateAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.UpdateAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TBLLEntity entity, object? userId = null)
        {
            var dalEntity = Mapper.Map(entity);
            var resultDALEntity = await Repository.RemoveAsync(dalEntity, userId);
            var result = Mapper.Map(resultDALEntity);
            
            return result;
        }

        public virtual async Task<TBLLEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var resultDALEntity = await Repository.RemoveAsync(id, userId);
            var result = Mapper.Map(resultDALEntity);
            
            return result;
        }

        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var result = await Repository.ExistsAsync(id, userId);
            return result;
        }
    }
}