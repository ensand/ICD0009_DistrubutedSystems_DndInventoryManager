using System;
using Domain.App.Identity;
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
            
        }
        
        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };

            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed: " + roleName);
                    }
                }
            }
            
            var users = new (string email, string password, string nickname, Guid Id)[]
            {
                ("enola@gmail.com", "password", "Enola", new Guid("00000000-0000-0000-0000-000000000001"))
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.email).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = userInfo.Id,
                        Email = userInfo.email,
                        UserName = userInfo.email,
                        Nickname = userInfo.nickname
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed: " + result.ToString());
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }
    }
}