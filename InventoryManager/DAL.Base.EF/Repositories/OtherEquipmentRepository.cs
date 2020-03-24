using Contracts.DAL.App.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class OtherEquipmentRepository : BaseRepository<OtherEquipment>, IOtherEquipmentRepository
    {
        public OtherEquipmentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}