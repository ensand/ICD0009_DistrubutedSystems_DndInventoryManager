using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseDbContext
    {
        public DbSet<DndCharacter> DndCharacters { get; set; } = default!;
        public DbSet<Weapon> Weapons { get; set; } = default!;
        public DbSet<Armor> Armors { get; set; } = default!;
        public DbSet<MagicalItem> MagicalItems { get; set; } = default!;
        public DbSet<OtherEquipment> OtherEquipments { get; set; } = default!;
        
        public Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> EntityTracker { get; } =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();
        

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public override int SaveChanges()
        {
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            
            return result;
        }


        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in EntityTracker)
            {
                value.Id = key.Id;
            }
        }
    }
}