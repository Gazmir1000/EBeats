using EBeats.Data;
using EBeats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBeats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeApiController : ControllerBase
    {
        private readonly EBeatsContext _context;
        public HomeApiController(EBeatsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                List<Category> groupedCategories = new List<Category>();
                foreach(Category c in categories)
                {
                    c.Products = await _context.Products.Where(p => p.CategoryId == c.Id).ToListAsync();
                    groupedCategories.Add(c);
                }
                return Ok(groupedCategories);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
