using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GalaTour.Models;
using Microsoft.EntityFrameworkCore;

namespace GalaTour
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
            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnection");
            // добавляем контекст ExcursionsContext в качестве сервиса в приложение
            services.AddDbContext<ExcursionContext>(options =>
                options.UseSqlServer(connection));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute("home", "/", new { controller = "Home", action = "Index" });
                routes.MapRoute("bus-tours", "bus-tours", new { controller = "Home", action = "Bus_tours" });
                routes.MapRoute("excursions", "excursions", new { controller = "Home", action = "Excursions" });
                routes.MapRoute("tours-abroad", "tours-abroad", new { controller = "Home", action = "Tours_abroad" });
                routes.MapRoute("contacts", "contacts", new { controller = "Home", action = "Contacts" });
                routes.MapRoute("about", "about", new { controller = "Home", action = "About" });
                routes.MapRoute("tourist", "tourist", new { controller = "Home", action = "Tourist" });


                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
