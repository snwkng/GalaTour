using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GalaTour.Models;
using Microsoft.EntityFrameworkCore;

namespace GalaTour.Controllers
{
    public class HomeController : Controller
    {
        private readonly ExcursionContext db;
        readonly DateTime date = DateTime.Today;
        public HomeController(ExcursionContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var ex = db.Excursions
                .Include(c => c.ExImage)
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice)
                .Where(c => c.ExDate.Date > date); // Вывод только предстоящих экскурсий
            ViewBag.eCity = ex.ToList();
            return View(ex.ToList());
        }
        public IActionResult Excursions()
        {
            ViewBag.SearchCount = db.Excursions
                    .Include(c => c.ID)
                    .Where(c => c.ExDate.Date > date).Count();
            ViewBag.eCity = db.Excursions
                   .Include(c => c.ExCity)
                   .Include(c => c.ExDate)
                   .Where(c => c.ExDate.Date > date).ToList();
            var ex = db.Excursions
                .Include(c => c.ExImage)
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice)
                .Where(c => c.ExDate.Date > date); // Вывод только предстоящих экскурсий
            return View(ex.ToList());
        }
        [HttpGet]
        public IActionResult Excursions(int id)
        {
            ViewBag.eCity = db.Excursions
                   .Include(c => c.ExCity)
                   .Include(c => c.ExDate)
                   .Where(c => c.ExDate.Date > date).ToList();
            if (id == 0)
            {
                ViewBag.SearchCount = db.Excursions
                    .Include(c => c.ID)
                    .Where(c => c.ExDate.Date > date).Count();
                var e = db.Excursions
                    .Include(c => c.ExImage)
                    .Include(c => c.ExDuration)
                    .Include(c => c.ExCity)
                    .Include(c => c.ExDate)
                    .Include(c => c.ExPrice)
                    .Where(c => c.ExDate.Date > date); // Вывод только предстоящих экскурсий
                return View(e.ToList());
            }
            ViewBag.SearchCount = db.Excursions
                    .Include(c => c.ID)
                    .Where(c => c.ExCity.ID == id)
                    .Where(c => c.ExDate.Date > date).Count();
            var ex = db.Excursions
                .Include(c => c.ExImage)
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice)
                .Where(c => c.ExCity.ID == id)
                .Where(c => c.ExDate.Date > date);
            return View(ex.ToList());
        }
        public IActionResult ExTour(int id)
        {
            var ex = db.Excursions
                .Include(c => c.ExImage)
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice)
                .Where(c => c.ExCity.ID == id).ToList();
            return View(ex);
        }
        public IActionResult BusTours()
        {
            return View();
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
