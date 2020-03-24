using Contracts.DAL.App.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class MagicalItemRepository : BaseRepository<MagicalItem>, IMagicalItemRepository
    {
        public MagicalItemRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}