using Contracts.DAL.App.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class DndCharacterRepository : BaseRepository<DndCharacter>, IDndCharacterRepository
    {
        public DndCharacterRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}