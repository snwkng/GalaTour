using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GalaTour.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GalaTour.Controllers
{
    public class AdminController : Controller
    {
        private readonly ExcursionContext _context;
        private readonly IHostingEnvironment _environment;

        public AdminController(ExcursionContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _environment = appEnvironment;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            var excursionContext = _context.Excursions.Include(e => e.ExCity).Include(e => e.ExDate).Include(e => e.ExDuration).Include(e => e.ExPrice);
            return View(await excursionContext.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excursion = await _context.Excursions
                .Include(e => e.ExCity)
                .Include(e => e.ExDate)
                .Include(e => e.ExDuration)
                .Include(e => e.ExPrice)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (excursion == null)
            {
                return NotFound();
            }

            return View(excursion);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["ImageURL"] = new SelectList(_context.Excursions, "ImageURL", "ImageURL");
            ViewData["ExCityID"] = new SelectList(_context.ExCityes, "ID", "City");
            ViewData["ExDateID"] = new SelectList(_context.ExDates, "ID", "Date");
            ViewData["ExDurationID"] = new SelectList(_context.ExDurations, "ID", "Duration");
            ViewData["ExPriceID"] = new SelectList(_context.ExPrices, "ID", "Price");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile NewImage, [Bind("ID,Name,Description,ImageURL,ExDurationID,ExCityID,ExDateID,ExPriceID")] Excursion excursion)
        {
            if (NewImage != null)
            {
                string path = "/images/" + NewImage.FileName;

                using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await NewImage.CopyToAsync(fileStream);
                }
                ViewData["ImageURL"] = path;
                ViewData["ImageURL"] = new SelectList(_context.Excursions, "ImageURL", "ImageURL", excursion.ImageURL);
            }
                if (ModelState.IsValid)
                {
                    _context.Add(excursion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ImageURL"] = new SelectList(_context.Excursions, "ImageURL", "ImageURL", excursion.ImageURL);
                ViewData["ExCityID"] = new SelectList(_context.ExCityes, "ID", "ID", excursion.ExCityID);
                ViewData["ExDateID"] = new SelectList(_context.ExDates, "ID", "ID", excursion.ExDateID);
                ViewData["ExDurationID"] = new SelectList(_context.ExDurations, "ID", "ID", excursion.ExDurationID);
                ViewData["ExPriceID"] = new SelectList(_context.ExPrices, "ID", "ID", excursion.ExPriceID);
            return View(excursion);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excursion = await _context.Excursions.FindAsync(id);
            if (excursion == null)
            {
                return NotFound();
            }
            ViewData["ExCityID"] = new SelectList(_context.ExCityes, "ID", "ID", excursion.ExCityID);
            ViewData["ExDateID"] = new SelectList(_context.ExDates, "ID", "ID", excursion.ExDateID);
            ViewData["ExDurationID"] = new SelectList(_context.ExDurations, "ID", "ID", excursion.ExDurationID);
            ViewData["ExPriceID"] = new SelectList(_context.ExPrices, "ID", "ID", excursion.ExPriceID);
            return View(excursion);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,ImageURL,ExDurationID,ExCityID,ExDateID,ExPriceID")] Excursion excursion)
        {
            if (id != excursion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(excursion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExcursionExists(excursion.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ExCityID"] = new SelectList(_context.ExCityes, "ID", "ID", excursion.ExCityID);
            ViewData["ExDateID"] = new SelectList(_context.ExDates, "ID", "ID", excursion.ExDateID);
            ViewData["ExDurationID"] = new SelectList(_context.ExDurations, "ID", "ID", excursion.ExDurationID);
            ViewData["ExPriceID"] = new SelectList(_context.ExPrices, "ID", "ID", excursion.ExPriceID);
            return View(excursion);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excursion = await _context.Excursions
                .Include(e => e.ExCity)
                .Include(e => e.ExDate)
                .Include(e => e.ExDuration)
                .Include(e => e.ExPrice)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (excursion == null)
            {
                return NotFound();
            }

            return View(excursion);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var excursion = await _context.Excursions.FindAsync(id);
            _context.Excursions.Remove(excursion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExcursionExists(int id)
        {
            return _context.Excursions.Any(e => e.ID == id);
        }
    }
}
