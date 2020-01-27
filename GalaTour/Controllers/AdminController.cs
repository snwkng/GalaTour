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
using GalaTour.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace GalaTour.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ExcursionContext _context;
        [Obsolete]
        private readonly IHostingEnvironment _appEnvironment;

        [Obsolete]
        public AdminController(ExcursionContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        /**** Логика добавления файлов ****/
        
        // GET: Admin/AddImage
        public IActionResult AddImage()
        {
            return View();
        }

        // Post: Admin/AddImage
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> AddImage(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/images/ex/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("ExcursionList", "Admin");
        }

        // GET: Admin/AddPrice
        public IActionResult AddPrice()
        {
            return View();
        }

        // Post: Admin/AddPrice
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> AddPrice(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/docs/ex/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("ExcursionList", "Admin");
        }

        /**** Конец ****/
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
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Email); // аутентификация

                    return RedirectToAction("Index", "Admin");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Admin");
        }
        /**** Конец ****/
        /******** Добавить город *******/

        // GET: AddCity
        public IActionResult AddCity()
        {
            var eCity = _context.ExCities.ToList();
            ViewBag.eCity = eCity;
            return View();
        }

        // POST: AddCity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity([Bind("ID,City")] ExCity exCity)
        {
            if(ModelState.IsValid)
            {
                _context.Add(exCity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ExcursionList));
            }
            return View(exCity);
        }

        /******** Конец ****************/
        // GET: Admin
        public async Task<IActionResult> ExcursionList()
        {
            var excursionContext = _context.Excursions.Include(e => e.ExCity);
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
            ViewData["CityID"] = new SelectList(_context.ExCities, "ID", "City");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,ThePriceInclude,ImageURL,Duration,ExCityID,Date,Price,HotelName,HotelLink,DocLink")] Excursion excursion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(excursion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ExcursionList));
            }
            ViewData["CityID"] = new SelectList(_context.ExCities, "ID", "ID", excursion.ExCityID);
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
            ViewData["CityID"] = new SelectList(_context.ExCities, "ID", "ID", excursion.ExCityID);
            return View(excursion);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,ThePriceInclude,ImageURL,Duration,ExCityID,Date,Price,HotelName,HotelLink,DocLink")] Excursion excursion)
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
                ViewData["CityID"] = new SelectList(_context.ExCities, "ID", "ID", excursion.ExCityID);
                return RedirectToAction(nameof(ExcursionList));
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
                .Include(e => e.ExCity)
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
            return RedirectToAction(nameof(ExcursionList));
        }

        private bool ExcursionExists(int id)
        {
            return _context.Excursions.Any(e => e.ID == id);
        }




        /** АВТОБУСНЫЕ ТУРЫ К МОРЮ **/

        // GET
        public IActionResult AddTourCity()
        {
            var tourCity = _context.TourCities.ToList();
            ViewBag.tourCity = tourCity;
            return View();
        }
        // POST: AddCity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTourCity([Bind("ID,City")] TourCity tourCity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourCity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BusTourList));
            }
            return View(tourCity);
        }
        // GET: AddRegion
        public IActionResult AddRegion()
        {
            return View();
        }
        // POST: AddRegion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRegion([Bind("ID,RegionName,RegionImage")] Region tourReg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourReg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BusTourList));
            }
            return View(tourReg);
        }
        public async Task<IActionResult> BusTourList()
        {
            var excursionContext = _context.BusTours.Include(e => e.TourCity).Include(e => e.Region);
            return View(await excursionContext.ToListAsync());
        }
        // GET: Admin/Details/5
        public async Task<IActionResult> DetailsTour(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busTour = await _context.BusTours.Include(e => e.TourCity).Include(e => e.Region)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busTour == null)
            {
                return NotFound();
            }

            return View(busTour);
        }

        // GET: Admin/Create
        public IActionResult CreateTour()
        {
            ViewData["TourCityID"] = new SelectList(_context.TourCities, "ID", "City");
            ViewData["RegionName"] = new SelectList(_context.Regions, "ID", "RegionName");
            ViewData["RegionImage"] = new SelectList(_context.Regions, "ID", "RegionImage");
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTour([Bind("ID,RegionID,RegionID,TourCityID,HotelType,HotelName,Description,AddInfo,Price,Date,DocLink,HotelImage")] BusTour busTour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(busTour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(BusTourList));
            }
            ViewData["TourCityID"] = new SelectList(_context.TourCities, "ID", "ID", busTour.TourCityID);
            ViewData["RegionName"] = new SelectList(_context.Regions, "ID", "RegionName", busTour.RegionID);
            ViewData["RegionImage"] = new SelectList(_context.Regions, "ID", "RegionImage", busTour.RegionID);
            return View(busTour);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> EditTour(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busTour = await _context.BusTours.FindAsync(id);
            if (busTour == null)
            {
                return NotFound();
            }
            ViewData["TourCityID"] = new SelectList(_context.TourCities, "ID", "City", busTour.TourCityID);
            ViewData["RegionName"] = new SelectList(_context.Regions, "ID", "RegionName", busTour.RegionID);
            ViewData["RegionImage"] = new SelectList(_context.Regions, "ID", "RegionImage", busTour.RegionID);
            return View(busTour);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTour(int id, [Bind("ID,RegionID,RegionID,TourCityID,HotelType,HotelName,Description,AddInfo,Price,Date,DocLink,HotelImage")] BusTour busTour)
        {
            if (id != busTour.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(busTour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusTourExist(busTour.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["TourCityID"] = new SelectList(_context.TourCities, "ID", "ID", busTour.TourCityID);
                ViewData["RegionID"] = new SelectList(_context.Regions, "ID", "ID", busTour.RegionID);
                return RedirectToAction(nameof(BusTourList));
            }
            return View(busTour);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> DeleteTour(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var busTour = await _context.BusTours
                .FirstOrDefaultAsync(m => m.ID == id);
            if (busTour == null)
            {
                return NotFound();
            }

            return View(busTour);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("DeleteTour")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTourConfirmed(int id)
        {
            var busTour = await _context.BusTours.FindAsync(id);
            _context.BusTours.Remove(busTour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BusTourList));
        }

        private bool BusTourExist(int id)
        {
            return _context.BusTours.Any(e => e.ID == id);
        }

        public IActionResult Index()
        {
            return View();
        }

        /**** Логика добавления файлов ТУРОВ К МОРЮ****/

        // GET: Admin/AddImage
        public IActionResult AddImageTour()
        {
            return View();
        }

        // Post: Admin/AddImage
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> AddImageTour(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/images/tour/hotels/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("BusTourList", "Admin");
        }

        // GET: Admin/AddPrice
        public IActionResult AddPriceTour()
        {
            return View();
        }

        // Post: Admin/AddPrice
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> AddPriceTour(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/docs/tours/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
                _context.SaveChanges();
            }

            return RedirectToAction("BusTourList", "Admin");
        }

        /**** Конец ****/

    }
}
