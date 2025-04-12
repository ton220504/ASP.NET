using Microsoft.AspNetCore.Mvc;
using TranVanToan_2122110162.Data;
using TranVanToan_2122110162.Models;
using System.Linq;

namespace TranVanToan_2122110162.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrandController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Brand
        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = _context.Brands.ToList();
            return Ok(brands);
        }

        // GET: api/Brand/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
                return NotFound("Brand not found");
            return Ok(brand);
        }

        // POST: api/Brand
        [HttpPost]
        public IActionResult Create([FromBody] BrandDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var brand = new Brand
            {
                BrandName = dto.BrandName
            };

            _context.Brands.Add(brand);
            _context.SaveChanges();

            return Ok(brand);
        }

        // PUT: api/Brand/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] BrandDto dto)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
                return NotFound("Brand not found");

            brand.BrandName = dto.BrandName;
            _context.SaveChanges();

            return Ok(brand);
        }

        // DELETE: api/Brand/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.BrandId == id);
            if (brand == null)
                return NotFound("Brand not found");

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return Ok(new { message = "Brand deleted." });
        }
    }
}
