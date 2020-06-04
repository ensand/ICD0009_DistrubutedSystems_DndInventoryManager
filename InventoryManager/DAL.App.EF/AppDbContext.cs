using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using com.enola.inventorymanager.Contracts.DAL.Base;
using com.enola.inventorymanager.Contracts.Domain;
using Domain.App;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
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
        
            
            builder.Entity<DndCharacter>()
                .HasMany(c => c.Armor)
                .WithOne(c => c.DndCharacter!)
                .HasForeignKey(oe => oe.DndCharacterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<DndCharacter>()
                .HasMany(c => c.Weapons)
                .WithOne(c => c.DndCharacter!)
                .HasForeignKey(oe => oe.DndCharacterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<DndCharacter>()
                .HasMany(c => c.MagicalItems)
                .WithOne(c => c.DndCharacter!)
                .HasForeignKey(oe => oe.DndCharacterId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<DndCharacter>()
                .HasMany(c => c.OtherEquipment)
                .WithOne(oe => oe.DndCharacter!)
                .HasForeignKey(oe => oe.DndCharacterId)
                .OnDelete(DeleteBehavior.Cascade);
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

        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            EntityTracker.Add(internalEntity, externalEntity);
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