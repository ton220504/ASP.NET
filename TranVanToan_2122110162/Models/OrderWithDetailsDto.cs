namespace TranVanToan_2122110162.Models
{
    public class OrderWithDetailsDto
    {
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    
}
