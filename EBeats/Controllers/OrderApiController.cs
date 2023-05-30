using EBeats.Data;
using EBeats.Models;
using Microsoft.AspNetCore.Mvc;

namespace EBeats.Controllers

{
[ApiController]
[Route("api/[controller]")]
public class OrderApiController : ControllerBase
    {
    private readonly EBeatsContext _context;
    public OrderApiController(EBeatsContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> Krijo(Order o)
    {
        try
        {
            _context.Orders.Add(o);
            await _context.SaveChangesAsync();
            return Ok("Order Added");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}
}
