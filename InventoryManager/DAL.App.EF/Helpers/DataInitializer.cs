using System;
using System.Linq;
using Domain.App;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializer
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }
        
        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            var userName = "enola1998@gmail.com";
            
            // ReSharper disable StringLiteralTypo
            var dndCharacter = new DndCharacter()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    AppUserId = userManager.FindByNameAsync(userName).Result.Id,
                    Name = "Dingdong Pingpong",
                    Comment = "Default character",
                    PlatinumPieces = 0,
                    GoldPieces = 420,
                    ElectrumPieces =  0,
                    SilverPieces = 0,
                    CopperPieces = 6372
            };
            // ReSharper enable StringLiteralTypo

            if (!context.DndCharacters.Any(c => c.Id == dndCharacter.Id))
            {
                context.DndCharacters.Add(dndCharacter);
                context.SaveChanges();
            }
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleNames = new [] {"user", "admin"};

            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
            
                if (role == null)
                {
                    role = new AppRole {Name = roleName};
                    var result = roleManager.CreateAsync(role).Result;
                
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed: " + roleName);
                    }
                }
            }
        }
    }
}