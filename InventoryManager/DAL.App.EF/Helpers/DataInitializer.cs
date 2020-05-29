using System;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializer
    {
        public static void MigrateDatabase(AppEntityTracker context)
        {
            context.Database.Migrate();
        }
        
        public static bool DeleteDatabase(AppEntityTracker context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedData(AppEntityTracker context, UserManager<AppUser> userManager)
        {
            var userName = "enola1998@gmail.com";
            
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

            if (!context.DndCharacters.Any(c => c.Id == dndCharacter.Id))
            {
                context.DndCharacters.Add(dndCharacter);
                context.SaveChanges();
            }
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roleNames = new string[] {"user", "admin"};

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

            var userName = "enola1998@gmail.com";
            var password = "_Kibuvitsa196";
            var firstName = "Enola";
            var lastName = "Sander";
            
            var user = userManager.FindByNameAsync(userName).Result;
            
            if (user == null)
            {
                user = new AppUser
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"), 
                    Email = userName, 
                    UserName = userName, 
                    FirstName = firstName, 
                    LastName = lastName
                };
                var result = userManager.CreateAsync(user, password).Result;

                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed: " + userName);
                }
            }

            var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "user").Result;
        }
    }
}