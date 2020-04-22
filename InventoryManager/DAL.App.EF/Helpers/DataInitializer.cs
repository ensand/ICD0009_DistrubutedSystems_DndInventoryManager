using System;
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
        
        public static void SeedData(AppDbContext context)
        {
            
        }
    }
}