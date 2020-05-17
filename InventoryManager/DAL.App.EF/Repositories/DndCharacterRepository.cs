using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.V1;

namespace DAL.App.EF.Repositories
{
    public class DndCharacterRepository : EFBaseRepository<AppDbContext, Domain.DndCharacter, DAL.App.DTO.DndCharacter>, IDndCharacterRepository
    {
        public DndCharacterRepository(AppDbContext dbContext) 
            : base(dbContext, new BaseDALMapper<Domain.DndCharacter, DAL.App.DTO.DndCharacter>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.DndCharacter>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }
            
            return (await RepoDbSet
                    .Where(d => d.AppUserId == userId)
                    .Select(dbEntity => new DAL.App.DTO.DndCharacterSummary()
                    {
                        Id = dbEntity.Id,
                        Name = dbEntity.Name,
                        Comment = dbEntity.Comment,
                        ArmorCount = dbEntity.Armor!.Count,
                        MagicalItemCount = dbEntity.MagicalItems!.Count,
                        OtherEquipmentCount = dbEntity.OtherEquipment!.Count,
                        WeaponCount = dbEntity.Weapons!.Count,
                        TreasureInGp = (float) dbEntity.PlatinumPieces * 10 + 
                                       (float) dbEntity.GoldPieces + 
                                       (float) dbEntity.ElectrumPieces / 2 + 
                                       (float) dbEntity.SilverPieces / 10 + 
                                       (float) dbEntity.CopperPieces / 100
                    })
                    .ToListAsync())
                .Select(dbEntity => Mapper.Map<DAL.App.DTO.DndCharacterSummary, DAL.App.DTO.DndCharacter>(dbEntity));
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