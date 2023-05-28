using EBeats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using EBeats.Data;

namespace EBeats.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public HttpContext httpContext;


        [HttpGet]
        public IActionResult FormaTeDhenaPersonales()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FormaTeDhenaPersonales(Guest guest)
        {
            HttpContext.Session.SetString("Pajisja", guest.Pajisja);
            HttpContext.Session.SetString("Ngjyra", guest.Ngjyra);
            HttpContext.Session.SetInt32("Cmimi", (int)guest.Cmimi);
            return RedirectToAction("Profil");
        }

        public IActionResult Profil()

        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("Pajisja")) &&
            !String.IsNullOrEmpty(HttpContext.Session.GetString("Ngjyra")) &&
            HttpContext.Session.GetInt32("Cmimi") != 0)
            {
                var guest = new Guest
                {
                    Pajisja = HttpContext.Session.GetString("Pajisja"),
                    Ngjyra = HttpContext.Session.GetString("Ngjyra"),
                    Cmimi = (int)HttpContext.Session.GetInt32("Cmimi")
                };
                return View(guest);
            }
            return RedirectToAction("FormaTeDhenaPersonales");
        }

        public IActionResult Index(ApplicationUser context)
        {
      
            return View();
        }

        private readonly ApplicationUser _context;

        


        public IActionResult Privacy()
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