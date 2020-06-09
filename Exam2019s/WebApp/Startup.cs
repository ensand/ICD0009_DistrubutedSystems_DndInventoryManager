using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DAL.App.EF;
using Domain.App.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("MsSqlConnection")));
            
            // Scoped dependencies go here
            
            
            
            services.AddIdentity<AppUser, AppRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            });
            
            
            services.AddControllersWithViews();
            services.AddRazorPages();
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // Removes default claims
            services.AddAuthentication()
                .AddCookie(options => { options.SlidingExpiration = true; })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JWT:issuer"],
                        ValidAudience = Configuration["JWT:issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:signingKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app, env, Configuration);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseCors("CorsAllowAll");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        
        private void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)

        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
            var logger = serviceScope.ServiceProvider.GetService<ILogger<Startup>>();


            if (Configuration["AppDataInitialization:DropDataBase"] == "True")
            {
                logger.LogInformation("AppDataInitialization:DropDataBase");
                DAL.App.EF.Helpers.DataInitializer.DeleteDatabase(context);
            }

            if (Configuration["AppDataInitialization:MigrateDataBase"] == "True")
            {
                logger.LogInformation("AppDataInitialization:MigrateDataBase");
                DAL.App.EF.Helpers.DataInitializer.MigrateDatabase(context);
            }

            if (Configuration["AppDataInitialization:SeedIdentity"] == "True")
            {
                logger.LogInformation("AppDataInitialization:SeedIdentity");
                DAL.App.EF.Helpers.DataInitializer.SeedIdentity(userManager, roleManager);
            }

            if (Configuration.GetValue<bool>("AppDataInitialization:SeedData"))
            {
                logger.LogInformation("AppDataInitialization:SeedData");
                DAL.App.EF.Helpers.DataInitializer.SeedData(context, userManager);
            }
        }
    }
}