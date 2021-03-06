using System;
using System.Threading.Tasks;
using com.enola.inventorymanager.DAL.Base;
using Microsoft.EntityFrameworkCore;


namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TKey, TDbContext> : BaseUnitOfWork<TKey>
        where TDbContext : DbContext 
        where TKey : IEquatable<TKey>
    {
        protected readonly TDbContext UowDbContext;
        
        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }

        public override async Task<int> SaveChangesAsync()
        {
            var result = await UowDbContext.SaveChangesAsync();
             
            UpdateTrackedEntities();
             
            return result;
        }
    }
}