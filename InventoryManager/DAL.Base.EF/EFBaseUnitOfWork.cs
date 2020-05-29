using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TDbContext> : BaseUnitOfWork
        where TDbContext : DbContext
    {
        protected readonly TDbContext UowEntityTracker;
        
        public EFBaseUnitOfWork(TDbContext uowEntityTracker)
        {
            UowEntityTracker = uowEntityTracker;
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await UowEntityTracker.SaveChangesAsync();
        }
    }
}