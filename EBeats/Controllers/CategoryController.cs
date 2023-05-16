using Microsoft.AspNetCore.Mvc;

namespace EBeats.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
