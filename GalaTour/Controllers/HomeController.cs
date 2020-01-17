﻿using System;
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
                .Select(c => c.ExCity);
            ViewBag.eCity = eCity;
            // автобусные туры к морю:
            var busTour = db.BusTours.ToList();
            ViewBag.busTour = busTour;
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
                .Select(c => c.ExCity).ToList();
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
                .Select(c => c.ExCity).ToList();
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
