using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<DndCharacter> DndCharacters { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<MagicalItem> MagicalItems { get; set; }
        public DbSet<OtherEquipment> OtherEquipments { get; set; }
        public DbSet<CharactersWeapons> CharactersWeapons { get; set; }
        public DbSet<CharactersArmor> CharactersArmors { get; set; }
        public DbSet<CharactersMagicalItems> CharactersMagicalItems { get; set; }
        public DbSet<CharactersEquipment> CharactersEquipments { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}