using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BLL.App;
using Contracts.BLL.App;
using Contracts.DAL.App;
using DAL.App.EF;
using Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using WebApp.Helpers;

namespace WebApp
{
    /// <summary>
    /// The main method for configuring the application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Custom configuration, appsettings.json
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Method for configuring services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MsSqlConnection")));

            // DEPENDENCY INJECTION. Add as scoped dependency, interface gets tied to the implementation.
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
            services.AddScoped<IAppBLL, AppBLL>();
            
            services.AddIdentity<AppUser, AppRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            
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
            
            services.AddApiVersioning(options => { options.ReportApiVersions = true; });
            
            services.AddVersionedApiExplorer( options => options.GroupNameFormat = "'v'VVV" );
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen();
        }
        
        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
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
            
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach ( var description in provider.ApiVersionDescriptions )
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant() );
                    }
                } );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration Configuration)
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