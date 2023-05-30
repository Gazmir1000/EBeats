using EBeats.Data;
using EBeats.Models;
using Microsoft.AspNetCore.Mvc;

namespace EBeats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class ShippingApiController : Controller
    {
        private readonly EBeatsContext _context;
        public ShippingApiController(EBeatsContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Krijo(Shipping s)
        {
            try
            {
                _context.Shippings.Add(s);
                await _context.SaveChangesAsync();
                return Ok("Shipping Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
