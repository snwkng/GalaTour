using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.HttpsPolicy;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Cache-Control", "public, max-age=31536000");
                }
            });
            app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");
            //app.UseStatusCodePages();
            //app.UseHttpsRedirection();
            app.UseHttpMethodOverride();
            app.UseCookiePolicy();
            app.UseAuthentication();


            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
           {
               endpoints.MapControllerRoute("home", "/", new { controller = "Home", action = "Index" });
                endpoints.MapControllerRoute("bus-tours", "bus-tours", new { controller = "Home", action = "BusTours" });
                endpoints.MapControllerRoute("excursions", "excursions", new { controller = "Home", action = "Excursions" });
                endpoints.MapControllerRoute("excursion-tour", "excursion/{id:int?}", new { controller = "Home", action = "ExTour" });
                endpoints.MapControllerRoute("tours-abroad", "tours-abroad", new { controller = "Home", action = "ToursAbroad" });
                endpoints.MapControllerRoute("tour-search", "tour-search", new { controller = "Home", action = "TourSearch" });
                endpoints.MapControllerRoute("contacts", "contacts", new { controller = "Home", action = "Contacts" });
                endpoints.MapControllerRoute("about", "about", new { controller = "Home", action = "About" });
                endpoints.MapControllerRoute("tourist", "tourist", new { controller = "Home", action = "Tourist" });
                endpoints.MapControllerRoute("privacy-policy", "privacy-policy", new { controller = "Home", action = "PrivacyPolicy" });
                endpoints.MapControllerRoute("agreement", "agreement", new { controller = "Home", action = "Agreement" });

                endpoints.MapControllerRoute(
                   "bus-tours-region",
                   "bus-tours/{regName}",
                   new { controller = "Home", action = "Region" }
               );
                endpoints.MapControllerRoute(
                   "bus-tours-city",
                   "bus-tours/{City}",
                   new { controller = "Home", action = "BusTours" }
               );
                endpoints.MapControllerRoute(
                   "bus-tours-hotel",
                   "bus-tours/{regName}/{city}/{hotelName}",
                   new { controller = "Home", action = "Hotel" }
               );

               endpoints.MapDefaultControllerRoute();
           });
        }
    }
}
