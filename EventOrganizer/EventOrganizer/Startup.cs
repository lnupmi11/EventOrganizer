using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventOrganizer.Data.Models;
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
            services.AddDbContext<AppIdentityDbContext>(options =>
                 options.UseSqlServer(
                 Configuration["Data:EventOrganizerIdentity:ConnectionString"]));
                            services.AddIdentity<User, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>()
                            .AddDefaultTokenProviders();

            services.AddTransient<IEventRepository, MockEventRepository>();
            services.AddTransient<ICategoryRepository, MockCategoryRepository>();

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
        }
    }
}
