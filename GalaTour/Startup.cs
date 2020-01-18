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
using Microsoft.AspNetCore.Authentication.Cookies;

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
        [Obsolete]
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
            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Admin/Login");
                });

            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
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
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("home", "/", new { controller = "Home", action = "Index" });
                routes.MapRoute("bus-tours", "bus-tours", new { controller = "Home", action = "BusTours" });
                routes.MapRoute("excursions", "excursions", new { controller = "Home", action = "Excursions" });
                routes.MapRoute("excursion-tour", "excursion/{id:int?}", new { controller = "Home", action = "ExTour" });
                routes.MapRoute("tours-abroad", "tours-abroad", new { controller = "Home", action = "ToursAbroad" });
                routes.MapRoute("contacts", "contacts", new { controller = "Home", action = "Contacts" });
                routes.MapRoute("about", "about", new { controller = "Home", action = "About" });
                routes.MapRoute("tourist", "tourist", new { controller = "Home", action = "Tourist" });
                routes.MapRoute("privacy-policy", "privacy-policy", new { controller = "Home", action = "PrivacyPolicy" });
                routes.MapRoute("agreement", "agreement", new { controller = "Home", action = "Agreement" });

                routes.MapRoute(
                    "bus-tours-region",
                    "bus-tours/{regName}",
                    new { controller = "Home", action = "Region" }
                );
                routes.MapRoute(
                    "bus-tours-city",
                    "bus-tours/{regName}/{tourCity}",
                    new { controller = "Home", action="TourCity" }
                );

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
