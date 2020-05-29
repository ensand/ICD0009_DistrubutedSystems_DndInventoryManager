using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.Base.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class EFBaseRepository<TDbContext, TUser, TDomainEntity, TDALEntity> : 
        EFBaseRepository<Guid, TDbContext, TUser, TDomainEntity, TDALEntity>, 
        IBaseRepository<TDALEntity>
    
        where TDbContext : DbContext, IBaseDbContext
        where TUser : IdentityUser<Guid>
        where TDomainEntity : class, IDomainEntityId<Guid>, new()
        where TDALEntity : class, IDomainEntityId<Guid>, new()
    {
        public EFBaseRepository(TDbContext repoDbContext, IBaseMapper<TDomainEntity, TDALEntity> mapper) : base(repoDbContext, mapper)
        {
        }
    }
    
    public class EFBaseRepository<TKey, TDbContext, TUser, TDomainEntity, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext, IBaseDbContext<TKey>
        where TUser : IdentityUser<TKey>
        where TDomainEntity : class, IDomainEntityId<TKey>, new()
        where TDALEntity : class, IDomainEntityId<TKey>, new()
    {
        // ReSharper disable MemberCanBePrivate.Global
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TDomainEntity> RepoDbSet;
        protected readonly IBaseMapper<TDomainEntity, TDALEntity> Mapper;
        // ReSharper enable MemberCanBePrivate.Global


        public EFBaseRepository(TDbContext repoDbContext, IBaseMapper<TDomainEntity, TDALEntity> mapper)
        {
            RepoDbContext = repoDbContext;
            RepoDbSet = RepoDbContext.Set<TDomainEntity>();
            Mapper = mapper;

            if (RepoDbSet == null)
                throw new ArgumentNullException(typeof(TDALEntity).Name + "was not found as DbSet.");
        }

        public virtual async Task<IEnumerable<TDALEntity>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            
            return result;

        }

        public virtual async Task<TDALEntity> FirstOrDefaultAsync(TKey id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);

            return result;
        }

        public virtual TDALEntity Add(TDALEntity entity)
        {
            var domainEntity = Mapper.Map(entity);
            var trackedDomainEntity = RepoDbSet.Add(domainEntity).Entity;
            
            RepoDbContext.EntityTracker.Add(trackedDomainEntity, entity);
            
            var result = Mapper.Map(trackedDomainEntity);
            
            return result;
        }

        public virtual async Task<TDALEntity> UpdateAsync(TDALEntity entity, object? userId = null)
        {
            var domainEntity = Mapper.Map(entity);
            
            await CheckDomainEntityOwnership(domainEntity, userId);
            
            var trackedDomainEntity = RepoDbSet.Update(domainEntity).Entity;
            var result = Mapper.Map(trackedDomainEntity);
            
            return result;
        }

        public virtual async Task<TDALEntity> RemoveAsync(TDALEntity entity, object? userId = null)
        {
            var domainEntity = Mapper.Map(entity);
            await CheckDomainEntityOwnership(domainEntity, userId);

            var result = Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);

            return result;
        }

        public virtual async Task<TDALEntity> RemoveAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId, true);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));

            if (domainEntity == null)
                throw new ArgumentException("Entity to be updated was not found in data source.");

            var result = Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);

            return result;
        }

        // TODO: figure out why 'ExistsAsync' closes the db connection
        // What I have tried: just calling this out, saving the result to a variable.
        public virtual async Task<bool> ExistsAsync(TKey id, object? userId = null)
        {
            var query = PrepareQuery(userId, true);
            var recordExists = await query.AnyAsync(e => e.Id.Equals(id));
            
            return recordExists;
        }

        protected IQueryable<TDomainEntity> PrepareQuery(object? userId = null, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            
            // Shall we disable entity tracking
            if (noTracking)
                query = query.AsNoTracking();

            // userId != null and is this entity implementing IDomainEntityUser
            if (userId != null && typeof(IDomainEntityUser<TKey, TUser>).IsAssignableFrom(typeof(TDomainEntity)))
            {
                // accessing TDomainEntity.AppUserId via shadow property access
                query = query.Where(e =>
                    Microsoft.EntityFrameworkCore.EF.Property<TKey>(e, nameof(IDomainEntityUser<TKey, TUser>.AppUserId))
                        .Equals((TKey) userId));
            }

            Console.WriteLine("Repo: " + query.ToArray() + " - " + query.ToArray().Length);

            return query;
        }

        protected async Task CheckDomainEntityOwnership(TDomainEntity entity, object? userId = null)
        {
            var recordExists = await ExistsAsync(entity.Id, userId);
            
            if (!recordExists)
                throw new ArgumentException("Entity to be updated was not found in data source.");
        }
    }
}