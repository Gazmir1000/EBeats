using EBeats.Data;
using EBeats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBeats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductApiController : ControllerBase
    {
        private readonly EBeatsContext _context;
        public ProductApiController(EBeatsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _context.Products.ToListAsync();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{search}")]
        public async Task<IActionResult> Search(string search)
        {
            try
            {
                var data = await _context.Products.FirstOrDefaultAsync(el=>el.Name.ToLower() == search.ToLower());
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //post

        [HttpPost]
        public async Task<IActionResult> Krijo(Product p)
        {
            try
            {
                _context.Products.Add(p);
                await _context.SaveChangesAsync();
                return Ok("Product Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //delete
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var l = await _context.Products.FindAsync(id);
                if (l == null)
                {
                    return BadRequest("Nuk ekziston produkt me kete id");
                }
                else
                {
                    _context.Products.Remove(l);
                    await _context.SaveChangesAsync();
                    return Ok("Produkti u fshi me sukses");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Ndrysho(int id, [FromBody] Product editedProduct)
        {
            try
            {
                var pr = _context.Products.FirstOrDefault(p => p.Id == id);
                if (pr == null)
                {
                    return NotFound();
                }
                pr.Name = editedProduct.Name;
                pr.Sale = editedProduct.Sale;
                pr.Price = editedProduct.Price;
                pr.Description = editedProduct.Description;
                pr.Image = editedProduct.Image; 
                pr.CategoryId = editedProduct.CategoryId;   
                await _context.SaveChangesAsync();
                return Ok("Produkt Changed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
