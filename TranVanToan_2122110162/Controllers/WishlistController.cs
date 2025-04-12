using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranVanToan_2122110162.Data;
using TranVanToan_2122110162.Models;

namespace TranVanToan_2122110162.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WishlistController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var wishlists = _context.Wishlists
                .Include(w => w.User)
                .Include(w => w.Product)
                .ToList();

            return Ok(wishlists);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var wishlist = _context.Wishlists
                .Include(w => w.User)
                .Include(w => w.Product)
                .FirstOrDefault(w => w.WishlistId == id);

            if (wishlist == null)
                return NotFound("Wishlist not found.");

            return Ok(wishlist);
        }

        [HttpPost]
        public IActionResult Create([FromBody] WishlistDto dto)
        {
            var wishlist = new Wishlist
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId
            };

            _context.Wishlists.Add(wishlist);
            _context.SaveChanges();

            return Ok(wishlist);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WishlistDto dto)
        {
            var wishlist = _context.Wishlists.FirstOrDefault(w => w.WishlistId == id);
            if (wishlist == null)
                return NotFound("Wishlist not found.");

            wishlist.UserId = dto.UserId;
            wishlist.ProductId = dto.ProductId;

            _context.SaveChanges();
            return Ok(wishlist);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var wishlist = _context.Wishlists.FirstOrDefault(w => w.WishlistId == id);
            if (wishlist == null)
                return NotFound("Wishlist not found.");

            _context.Wishlists.Remove(wishlist);
            _context.SaveChanges();

            return Ok(new { message = "Wishlist deleted successfully." });
        }
    }
}
