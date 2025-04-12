using Microsoft.AspNetCore.Mvc;
using TranVanToan_2122110162.Data;
using TranVanToan_2122110162.Models;
using System.Linq;

namespace TranVanToan_2122110162.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Category
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        // GET: api/Category/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return NotFound("Category not found");
            return Ok(category);
        }

        // POST: api/Category
        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = new Category
            {
                CategoryName = dto.CategoryName
            };

            _context.Categories.Add(category);
            _context.SaveChanges();

            return Ok(category);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CategoryDto dto)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return NotFound("Category not found");

            category.CategoryName = dto.CategoryName;
            _context.SaveChanges();

            return Ok(category);
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
                return NotFound("Category not found");

            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Ok(new { message = "Category deleted." });
        }
    }
}
