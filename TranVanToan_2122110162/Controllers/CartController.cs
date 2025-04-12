using Microsoft.AspNetCore.Mvc;
using TranVanToan_2122110162.Data;
using TranVanToan_2122110162.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TranVanToan_2122110162.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cart
        [HttpGet]
        public IActionResult GetAll()
        {
            var carts = _context.Carts
                .Include(c => c.User)
                .Include(c => c.Product)
                .ToList();

            return Ok(carts);
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //var cart = _context.Carts.FirstOrDefault(c => c.CartId == id);
            var cart = _context.Carts
                .Include(w => w.User)
                .Include(w => w.Product)
                .FirstOrDefault(w => w.CartId == id);
            if (cart == null)
                return NotFound("Cart not found");
            return Ok(cart);
        }

        // POST: api/Cart
        [HttpPost]
        public IActionResult Create([FromBody] CartDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cart = new Cart
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();

            return Ok(cart);
        }

        // PUT: api/Cart/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CartDto dto)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CartId == id);
            if (cart == null)
                return NotFound("Cart not found");

            cart.UserId = dto.UserId;
            cart.ProductId = dto.ProductId;
            cart.Quantity = dto.Quantity;
            _context.SaveChanges();

            return Ok(cart);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cart = _context.Carts.FirstOrDefault(c => c.CartId == id);
            if (cart == null)
                return NotFound("Cart not found");

            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return Ok(new { message = "Cart deleted." });
        }
    }
}
