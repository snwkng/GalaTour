using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GalaTour.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace GalaTour.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ExcursionContext _context;

        private readonly string login = "galatour57@bus-orel.ru";
        private readonly string password = "galatour57";
        public AdminController(ExcursionContext context)
        {
            _context = context;
        }
        /**** Логика входа в админку *****/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            if (ModelState.IsValid)
            {
                if (login == Email && password == Password)
                {
                    await Authenticate(Email); // аутентификация

                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View();
        }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        /**** Конец ****/
        // GET: Admin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Excursions.ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var excursion = await _context.Excursions
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
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,ThePriceInclude,ImageURL,Duration,City,Date,Price,HotelName,HotelLink,DocLink")] Excursion excursion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(excursion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(excursion);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,ThePriceInclude,ImageURL,Duration,City,Date,Price,HotelName,HotelLink,DocLink")] Excursion excursion)
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
