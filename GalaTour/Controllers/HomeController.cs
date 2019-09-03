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
        private ExcursionContext db;
        public HomeController(ExcursionContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            Random random = new Random();
            var ex = db.Excursions
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice);
            ViewBag.eCity = db.ExCityes.ToList();
            return View(ex.ToList());
        }
        [HttpPost]
        public IActionResult Index(int CityID)
        {
            ViewBag.eCity = db.ExCityes.ToList();
            return RedirectToAction("Excursions", "Home", new { CityID }); // попробовать реализовать через post
        }
        public IActionResult Excursions()
        {
            ViewBag.eCity = db.ExCityes.ToList();
            var ex = db.Excursions
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice);
            return View(ex.ToList());
        }
        [HttpGet]
        public IActionResult Excursions(int cityID)
        {
            ViewBag.eCity = db.ExCityes.ToList();
            if (cityID == 0)
            {
                var e = db.Excursions
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice);
                return View(e.ToList());
            }
            var ex = db.Excursions
                .Include(c => c.ExDuration)
                .Include(c => c.ExCity)
                .Include(c => c.ExDate)
                .Include(c => c.ExPrice)
                .Where(c => c.ExCity.ID == cityID);
            return View(ex.ToList());
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Tourist()
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
