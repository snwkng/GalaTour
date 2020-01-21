using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GalaTour.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GalaTour.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExcursionContext db;
        [Obsolete]
        private readonly IHostingEnvironment _appEnvironment;
        readonly DateTime date = DateTime.Today;

        [Obsolete]
        public HomeController(ExcursionContext context, IHostingEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }
        [HttpPost]
        [Obsolete]
        public IActionResult GetFile(string FileName)
        {
            string file_type = "";
            if (FileName != null || FileName != "")
            {
                // Путь к файлу
                string file_path = Path.Combine(_appEnvironment.ContentRootPath, "wwwroot/docs/ex/" + FileName);
                // Тип файла - content-type
                if (FileName.Contains(".pdf"))
                {
                    file_type = "application/pdf";
                }
                if (FileName.Contains(".docx"))
                {
                    file_type = "application/docx";
                }
                if (FileName.Contains(".doc"))
                {
                    file_type = "application/doc";
                }
                if (FileName.Contains(".rtf"))
                {
                    file_type = "application/rtf";
                }
                // Имя файла - необязательно
                string file_name = FileName;
                return PhysicalFile(file_path, file_type, file_name);
            }
            return RedirectToAction("Excursions");
        }
        public IActionResult Index()
        {
            var ex = db.Excursions
                .Include(c => c.ExCity)
                .Where(c => c.Date > date); // Вывод только предстоящих экскурсий
            //var eCity = db.ExCities.ToList();
            var eCity = db.Excursions
                .Include(c => c.ExCity)
                .Select(c => c.ExCity).Distinct().ToList();
            ViewBag.eCity = eCity;
            // автобусные туры к морю:
            var busTourCity = db.BusTours.Include(c => c.TourCity).Select(c => c.TourCity).Distinct().ToList();
            ViewBag.busTourCity = busTourCity;
            var busTourRegion = db.BusTours.Include(c => c.Region).Select(c => c.Region).Distinct().ToList();
            ViewBag.busTourRegion = busTourRegion;
            return View(ex.ToList());
        }
        public IActionResult Excursions()
        {
            ViewBag.SearchCount = db.Excursions
                    .Include(c => c.ID)
                    .Include(c => c.ExCity)
                    .Where(c => c.Date > date).Count();
            var eCity = db.Excursions
                .Include(c => c.ExCity)
                .Select(c => c.ExCity).Distinct().ToList();
            ViewBag.eCity = eCity;
            var ex = db.Excursions
                .Include(c => c.ExCity)
                .Where(c => c.Date > date); // Вывод только предстоящих экскурсий
            return View(ex.ToList());
        }
        [HttpGet]
        public IActionResult Excursions(int id)
        {
            var eCity = db.Excursions
                .Include(c => c.ExCity)
                .Select(c => c.ExCity).Distinct().ToList();
            ViewBag.eCity = eCity;
            if (id == 0)
            {
                ViewBag.SearchCount = db.Excursions
                    .Include(c => c.ID)
                    .Include(c => c.ExCity)
                    .Where(c => c.Date > date).Count();
                var e = db.Excursions
                    .Include(c => c.ExCity)
                    .Where(c => c.Date > date); // Вывод только предстоящих экскурсий
                return View(e.ToList());
            }
            ViewBag.SearchCount = db.Excursions
                    .Include(c => c.ID)
                    .Include(c => c.ExCity)
                    .Where(c => c.ExCity.ID == id)
                    .Where(c => c.Date > date).Count();
            var ex = db.Excursions
                .Include(c => c.ExCity)
                .Where(c => c.ExCity.ID == id)
                .Where(c => c.Date > date);
            return View(ex.ToList());
        }
        public IActionResult ExTour(int id)
        {
            var ex = db.Excursions
                .Include(c => c.ExCity)
                .Where(c => c.ID == id).ToList();
            return View(ex);
        }
        public IActionResult BusTours() // Похоже, что этот контроллер никогда не запускается...
        {
            var busTour = db.BusTours
                .Include(c => c.Region)
                .Include(c => c.TourCity).ToList();
            var busTourCity = db.BusTours.Include(c => c.TourCity).Select(c => c.TourCity).Distinct().ToList();
            ViewBag.busTourCity = busTourCity;
            return View(busTour);
        }
        [HttpGet]
        public IActionResult BusTours(string City) 
        {
            ViewBag.CityName = "к морю";
            var busTour = db.BusTours
                .Include(c => c.Region)
                .Include(c => c.TourCity).ToList();
            if (City == "all")
            {
                 busTour = db.BusTours
                .Include(c => c.Region)
                .Include(c => c.TourCity).ToList();
            }
            else if (City != null)
            {
                ViewBag.CityName = "в " + City;
                busTour = db.BusTours
                .Include(c => c.Region)
                .Include(c => c.TourCity)
                .Where(c => c.TourCity.City == City).ToList();
            }
            var busTourCity = db.BusTours.Include(c => c.TourCity).Select(c => c.TourCity).Distinct().ToList();
            ViewBag.busTourCity = busTourCity;
            return View(busTour);
        }
        [HttpGet]
        public IActionResult Region(string regName)
        {
            ViewBag.RegionName = regName;
            var busTour = db.BusTours
                .Include(c => c.Region)
                .Include(c => c.TourCity)
                .Where(c => c.Region.RegionName == regName).ToList();
            var busTourCity = db.BusTours.Include(c => c.TourCity).Select(c => c.TourCity).Distinct().ToList();
            ViewBag.busTourCity = busTourCity;
            return View(busTour);
        }
        [HttpGet]
        public IActionResult Hotel(string regName, string city, string hotelName)
        {
            var busTour = db.BusTours
                 .Include(c => c.Region)
                 .Include(c => c.TourCity).ToList();
            return View(busTour);
        }
        public IActionResult Contacts()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Tourist()
        {
            return View();
        }
        public IActionResult ToursAbroad()
        {
            return View();
        }
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult Agreement()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
