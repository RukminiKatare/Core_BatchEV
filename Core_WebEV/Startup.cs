using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebEV.Models;
using Core_WebEV.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core_WebEV
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
            //register the DBContext in DI
            services.AddControllersWithViews();
            services.AddDbContext<CoreAppDBContext>((options) =>
            {
                //use sql server defined using the connection strings
                options.UseSqlServer(Configuration.GetConnectionString("AppDBConnection"));
            });

            //register repository classes in DI container
            services.AddScoped<IRepository<Category, int>, CategoryRepository>();
            services.AddScoped<IRepository<Product, int>, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
