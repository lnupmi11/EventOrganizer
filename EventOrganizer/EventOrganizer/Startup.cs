using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data;
using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Mocks;
using EventOrganizer.Data.Models;
using EventOrganizer.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using AspNetCore.RouteAnalyzer;

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

            services.AddRouteAnalyzer();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
          
            // List all possible routes
            //app.UseMvc(routes =>
            //{
            //    routes.MapRouteAnalyzer("/routes");
            //});

            DbInitializer.Seed(app);
        }
    }
}
