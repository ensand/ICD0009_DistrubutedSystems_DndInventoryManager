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
            
            var dndCharacter = new DndCharacter()
            {
                AppUserId = userManager.FindByNameAsync(userName).Result.Id,
                Name = "Dingdong",
                Comment = "___TEST",
                PlatinumPieces = 0,
                GoldPieces = 0,
                ElectrumPieces =  0,
                SilverPieces = 0,
                CopperPieces = 100000
            };

            var defaultExists = false;
            
            if (context.DndCharacters.Any())
            {
                foreach (var character in context.DndCharacters)
                {
                    if (character.Comment.Equals("___TEST") && character.Name.Equals("Dingdong"))
                    {
                        defaultExists = true;
                        break;
                    }
                    
                }
            }

            if (!defaultExists)
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
                user = new AppUser {Email = userName, UserName = userName, FirstName = firstName, LastName = lastName};
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