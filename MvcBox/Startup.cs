using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;
using System;
using Entities.Repository;
using System.Collections.Generic;

namespace MvcBox
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
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationUserContext>(options =>
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.UseSqlServer(Configuration.GetConnectionString("DEBContext"), x => x.MigrationsAssembly("Entities")));

            services.AddDbContext<SmartBoxContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DEBContext"), x => x.MigrationsAssembly("Entities")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
            }
            );

            services.AddDirectoryBrowser();
            services.AddCors();
            services.AddMvc()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressConsumesConstraintForFormFileParameters = true;
                    options.SuppressInferBindingSourcesForParameters = true;
                    options.SuppressModelStateInvalidFilter = true;
                    options.SuppressMapClientErrors = true;
                    options.ClientErrorMapping[404].Link =
                    "https://httpstatuses.com/404";
                }
                );
        }

        private async System.Threading.Tasks.Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            string[] roleNames = { "admin", "user", "driver" };
            IdentityResult roleResult;

            //Adding Leader Role
            foreach (var roleName in roleNames)
            {
                var roleCheck = await RoleManager.RoleExistsAsync(roleName);
                if (!roleCheck)
                {
                    //Create Role
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseCors(builder => builder.WithOrigins("https://localhost:44396"));

            app.UseDefaultFiles();

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(
            //    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
            //    RequestPath = "/wwwroot/css",
            //    ServeUnknownFileTypes = true
            //});

            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            

            app.UseFileServer(new FileServerOptions
                {
                    FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "download")),
                    RequestPath = "/download",
                    EnableDirectoryBrowsing = true
            });

            var IMEI_1 = Configuration.GetSection("TestContainer")["BoxId_1"];
            var IMEI_2 = Configuration.GetSection("TestContainer")["BoxId_2"];

            List<string> keys = new List<string>();
            keys.Add(IMEI_1);
            keys.Add(IMEI_2);

            var context = services.GetRequiredService<SmartBoxContext>();
            SeedData.InitializeTestContainer(context, keys);
            CreateUserRoles(services).Wait();
        }
    }
}
