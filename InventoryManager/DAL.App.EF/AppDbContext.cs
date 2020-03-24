using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<DndCharacter> DndCharacters { get; set; } = default!;
        public DbSet<Weapon> Weapons { get; set; } = default!;
        public DbSet<Armor> Armors { get; set; } = default!;
        public DbSet<MagicalItem> MagicalItems { get; set; } = default!;
        public DbSet<OtherEquipment> OtherEquipments { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}