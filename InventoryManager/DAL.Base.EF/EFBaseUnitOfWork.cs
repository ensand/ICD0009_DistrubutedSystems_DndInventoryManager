using System.Threading.Tasks;
using Contracts.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TDbContext> : BaseUnitOfWork, IBaseUnitOfWork
        where TDbContext : DbContext
    {
        protected TDbContext UowDbContext;
        
        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }

        public override int SaveChanges()
        {
            return UowDbContext.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await UowDbContext.SaveChangesAsync();
        }
    }
}