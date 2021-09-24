using Entities.Context;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System;
using Entities.Repository;
using System.Collections.Generic;
using Entities.Configuration;
using Entities.Interfaces;
using Entities.Repository.Base;
using MvcBox.Services.Interfaces;
using MvcBox.Services.UserService;
using Entities.Repository.Imitator;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MvcBox.AuthHelpers;
using MvcBox.Services;

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
            ConfigureAspnetRunServices(services);
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationUserContext>(options =>
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.UseSqlServer(Configuration.GetConnectionString("DEBContext"), x => x.MigrationsAssembly("Entities")));

            services.AddDbContext<SmartBoxContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DEBContext"), x => x.MigrationsAssembly("Entities")));

            services.AddDbContext<ImitatorContext>(options =>
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

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie()
            .AddJwtBearer(x =>
            {
                //x.Events = new JwtBearerEvents
                //{
                //    OnTokenValidated = context =>
                //    {
                //        var userService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                //        Guid userId;
                //        var result = Guid.TryParse(context.Principal.Identity.Name, out userId);
                //        var user = userService.GetUser(userId);
                //        if (user != null)
                //        {
                //            // return unauthorized if user no longer exists
                //            context.Fail("Unauthorized");
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthorization(options => options.AddPolicy("editContent", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser()
                    .RequireAssertion(context => context.User.HasClaim("CanEditContent", "true"))
                    .Build();
            }));
            
            //services.AddControllersWithViews().AddJsonOptions
            //(options =>
            //    options.JsonSerializerOptions.MaxDepth = 1024
            //);
        }

        /// <summary>
        /// Configure Services.
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureAspnetRunServices(IServiceCollection services)
        {
            //    services.AddControllers()
            //.AddControllersAsServices();
            // Add Core Layer
            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            //services.Configure<BaseProjectSettings>(Configuration.GetSection("BaseProjectSettings"));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            //services.AddScoped<IFileDataRepository, FileDataRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            // Add Application Layer
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<ILocationService, LocationService>();
            //services.AddScoped<IFileDataService, FileDataService>();

            // Add Web Layer
            services.AddAutoMapper(typeof(Startup)); // Add AutoMapper
            /*services.AddScoped<IIndexPageService, IndexPageService>();
            services.AddScoped<IProductPageService, ProductPageService>();
            services.AddScoped<ICategoryPageService, CategoryPageService>();*/

            // Add Miscellaneous
            services.AddHttpContextAccessor();
            /* services.AddHealthChecks()
                 .AddCheck<IndexPageHealthCheck>("home_page_health_check");*/
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

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

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
