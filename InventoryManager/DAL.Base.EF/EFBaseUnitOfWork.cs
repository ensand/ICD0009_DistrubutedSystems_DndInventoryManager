using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TDbContext> : BaseUnitOfWork
        where TDbContext : DbContext
    {
        protected readonly TDbContext UowDbContext;
        
        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await UowDbContext.SaveChangesAsync();
        }
    }
}