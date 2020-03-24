using Contracts.DAL.App.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class ArmorRepository : BaseRepository<Armor>, IArmorRepository
    {
        public ArmorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}