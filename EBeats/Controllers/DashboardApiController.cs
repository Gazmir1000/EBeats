using EBeats.Data;
using EBeats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBeats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardApiController : ControllerBase
    {
        private readonly EBeatsContext _context;
        public DashboardApiController(EBeatsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (_context!=null)
                {
                    var totalOrders = await _context.Orders.ToListAsync();
                    var pendingS = await _context.Shippings.Where(el => el.Status == Status.Pending).ToListAsync();
                    var inTransitShipping = await _context.Shippings.Where(el => el.Status == Status.InTransit).ToListAsync();
                    var outForDeliveryS = await _context.Shippings.Where(el => el.Status == Status.OutForDelivery).ToListAsync();
                    var deliveredS = await _context.Shippings.Where(el => el.Status == Status.Delivered).ToListAsync();
                    var canceledS = await _context.Shippings.Where(el => el.Status == Status.Cancelled).ToListAsync();
                    var data = new
                    {
                        totalOrder = totalOrders,
                        pending = pendingS,
                        inTransit = inTransitShipping,
                        outForDelivery = outForDeliveryS,
                        delivered = deliveredS,
                        canceled = canceledS

                    };

                    return Ok(data);

                }
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
