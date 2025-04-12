namespace TranVanToan_2122110162.Models
{
    public class ProductReviewDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
