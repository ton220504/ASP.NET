using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranVanToan_2122110162.Data;
using TranVanToan_2122110162.Models;

namespace TranVanToan_2122110162.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductReviewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var reviews = _context.ProductReviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .ToList();

            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var review = _context.ProductReviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .FirstOrDefault(r => r.ProductReviewId == id);

            if (review == null)
                return NotFound("Review not found.");

            return Ok(review);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductReviewDto dto)
        {
            var review = new ProductReview
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Comment = dto.Comment
            };

            _context.ProductReviews.Add(review);
            _context.SaveChanges();

            return Ok(review);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductReviewDto dto)
        {
            var review = _context.ProductReviews.FirstOrDefault(r => r.ProductReviewId == id);
            if (review == null)
                return NotFound("Review not found.");

            review.UserId = dto.UserId;
            review.ProductId = dto.ProductId;
            review.Rating = dto.Rating;
            review.Comment = dto.Comment;

            _context.SaveChanges();
            return Ok(review);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _context.ProductReviews.FirstOrDefault(r => r.ProductReviewId == id);
            if (review == null)
                return NotFound("Review not found.");

            _context.ProductReviews.Remove(review);
            _context.SaveChanges();

            return Ok(new { message = "Review deleted successfully." });
        }
    }
}
