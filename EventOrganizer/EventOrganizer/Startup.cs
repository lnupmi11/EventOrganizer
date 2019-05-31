using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AspNetCore.RouteAnalyzer;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Repositories;
using EventOrganizer.DAL.Models;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.BLL.Interfaces;
using EventOrganizer.BLL.Services;


namespace EventOrganizer
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940


        public void ConfigureServices(IServiceCollection services)
        {
            //Server configuration
            services.AddDbContext<EventOrganizerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("EventOrganizer")));

            services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<EventOrganizerDbContext>()
                            .AddDefaultTokenProviders();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IEventCartItemsRepository, EventCartItemsRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IEventCartItemsService, EventCartItemsService>();

            services.AddTransient<IUserService, UserService>();

            services.AddRouteAnalyzer();

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "categoryfilter",
                    template: "Events/{action}/{category?}",
                    defaults: new { Controller = "Events", action = "List" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{Id?}");
            });
            // List all possible routes
            //app.UseMvc(routes =>
            //{
            //    routes.MapRouteAnalyzer("/routes");
            //});

            DbInitializer.SeedRoles(roleManager);
            DbInitializer.SeedUsers(userManager);
            DbInitializer.Seed(app, userManager);
        }
    }
}
