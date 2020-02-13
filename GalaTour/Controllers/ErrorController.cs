using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GalaTour.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int StatusCode)
        {
            var feature = this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            ViewBag.StatusCode = StatusCode;
            return View(StatusCode);
        }
    }
}