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
    public class OrderDetailController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderDetailController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var details = _context.OrderDetails
                .Include(d => d.Order)
                .Include(d => d.Product)
                .ToList();
            return Ok(details);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var detail = _context.OrderDetails
                .Include(d => d.Order)
                .Include(d => d.Product)
                .FirstOrDefault(d => d.OrderDetailId == id);

            if (detail == null) return NotFound("Order detail not found");
            return Ok(detail);
        }
        [HttpGet("orderId/{orderId}")]
        public IActionResult GetOrderDetail(int orderId)
        {
            var orderDetails = _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Select(od => new
                {
                    od.OrderDetailId,
                    od.Quantity,
                    od.UnitPrice,
                    Product = new
                    {
                        od.Product.ProductId,
                        od.Product.ProductName,
                        od.Product.Description
                    }
                })
                .ToList();

            return Ok(orderDetails);
        }


        //[HttpPost]
        //public IActionResult Create([FromBody] OrderDetailDto dto)
        //{
        //    var detail = new OrderDetail
        //    {
        //        OrderId = dto.OrderId,
        //        ProductId = dto.ProductId,
        //        Quantity = dto.Quantity,
        //        UnitPrice = dto.UnitPrice
        //    };
        //    _context.OrderDetails.Add(detail);
        //    _context.SaveChanges();
        //    return Ok(detail);
        //}

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OrderDetailDto dto)
        {
            var detail = _context.OrderDetails.FirstOrDefault(d => d.OrderDetailId == id);
            if (detail == null) return NotFound("Order detail not found");

            detail.ProductId = dto.ProductId;
            detail.Quantity = dto.Quantity;
            detail.UnitPrice = dto.UnitPrice;

            _context.SaveChanges();
            return Ok(detail);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var detail = _context.OrderDetails.FirstOrDefault(d => d.OrderDetailId == id);
            if (detail == null) return NotFound("Order detail not found");

            _context.OrderDetails.Remove(detail);
            _context.SaveChanges();
            return Ok(new { message = "Order detail deleted." });
        }
    }
}
