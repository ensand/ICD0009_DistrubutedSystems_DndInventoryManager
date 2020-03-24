using Contracts.DAL.App.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class WeaponRepository : BaseRepository<Weapon>, IWeaponRepository
    {
        public WeaponRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}