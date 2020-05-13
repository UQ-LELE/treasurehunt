using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.DataLayer;

namespace treasurehunt.BackOffice.Web.UI
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
            services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(5));

            string connectionString = this.Configuration.GetConnectionString("DefaultContext");

            services.AddTransient<DalStoryEvent, DalStoryEvent>();
            services.AddTransient<DalChoice, DalChoice>();
            services.AddTransient<DalQuestion, DalQuestion>();
            services.AddTransient<DalEnemy, DalEnemy>();
            services.AddTransient<DalAvatar, DalAvatar>();
            services.AddTransient<DalHero, DalHero>();

            services.AddDbContext<DefaultContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
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
            app.UseSession();

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
