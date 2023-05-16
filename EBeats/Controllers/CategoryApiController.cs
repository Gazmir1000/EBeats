using EBeats.Data;
using EBeats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBeats.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryApiController : ControllerBase
    {
        private readonly EBeatsContext _context;
        public CategoryApiController(EBeatsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var data = await _context.Categories.ToListAsync();
                return Ok(data);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        //post

        [HttpPost]
        public async Task<IActionResult> Krijo(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return Ok("Category Added");
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
                var l = await _context.Categories.FindAsync(id);
                if (l == null)
                {
                    return BadRequest("Nuk ekziston kategri me kete id");
                }
                else
                {
                    _context.Categories.Remove(l);
                    await _context.SaveChangesAsync();
                    return Ok("Kategoria u fshi me sukses");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return NoContent();
            }
        }

        //put

        [HttpPut("{id}")]
        public async Task<IActionResult> Ndrysho(int id, [FromBody] Category updatedCategory)
        {
            try
            {
                var category = _context.Categories.FirstOrDefault(p => p.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                category.Name = updatedCategory.Name;
                await _context.SaveChangesAsync();
                return Ok("Category Changed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
