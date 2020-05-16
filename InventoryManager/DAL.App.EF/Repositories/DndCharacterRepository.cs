using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DndCharacterRepository : EFBaseRepository<AppDbContext, Domain.DndCharacter, DAL.App.DTO.DndCharacter>, IDndCharacterRepository
    {
        public DndCharacterRepository(AppDbContext dbContext) 
            : base(dbContext, new BaseDALMapper<Domain.DndCharacter, DAL.App.DTO.DndCharacter>())
        {
        }

        public Task<IEnumerable<DAL.App.DTO.DndCharacter>> AllAsync(Guid? userId = null)
        {
            // if (userId == null)
            // {
            //     return await base.AllAsync(); // base is not actually needed, using it for clarity
            // }
            //
            // return (await RepoDbSet
            //         .Where(o => o.AppUserId == userId)
            //         .Select(dbEntity => new OwnerDisplay()
            //         {
            //             Id = dbEntity.Id,
            //             FirstName = dbEntity.FirstName, 
            //             LastName = dbEntity.LastName,
            //             AnimalCount =  dbEntity.Animals!.Count
            //         })
            //         .ToListAsync())
            //     .Select(dbEntity => Mapper.Map<OwnerDisplay,DAL.App.DTO.Owner>(dbEntity));
            
            throw new NotImplementedException();
        }

        public async Task<DAL.App.DTO.DndCharacter> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
                return await RepoDbSet.AnyAsync(a => a.Id == id);

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var owner = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(owner.Id);
        }
    }
}