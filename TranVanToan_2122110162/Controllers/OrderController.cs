using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TranVanToan_2122110162.Data;
using TranVanToan_2122110162.Models;

namespace TranVanToan_2122110162.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _context.Orders.Include(o => o.User).ToList();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var order = _context.Orders.Include(o => o.User).FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound("Order not found");
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetOrderByUserId(int userId)
        {
            var orders = _context.Orders
                .Where(o => o.UserId == userId)
                .ToList();

            return Ok(orders);
        }


        //[HttpPost]
        //public IActionResult Create([FromBody] OrderDto dto)
        //{
        //    var order = new Order
        //    {
        //        OrderDate = dto.OrderDate,
        //        TotalAmount = dto.TotalAmount,
        //        UserId = dto.UserId
        //    };
        //    _context.Orders.Add(order);
        //    _context.SaveChanges();
        //    return Ok(order);
        //}

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OrderDto dto)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound("Order not found");

            order.OrderDate = dto.OrderDate;
            order.TotalAmount = dto.TotalAmount;
            order.UserId = dto.UserId;

            _context.SaveChanges();
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null) return NotFound("Order not found");

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok(new { message = "Order deleted." });
        }

        [HttpPost("with-details")]
        public IActionResult CreateOrderWithDetails([FromBody] OrderWithDetailsDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = dto.OrderDate,
                TotalAmount = dto.OrderDetails.Sum(d => d.Quantity * d.UnitPrice),
                OrderDetails = dto.OrderDetails.Select(d => new OrderDetail
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                    // Không cần set OrderId vì EF sẽ tự liên kết khi add OrderDetails vào Order
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok(order);
        }


    }
}
