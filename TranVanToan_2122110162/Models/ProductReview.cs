namespace TranVanToan_2122110162.Models
{
    public class ProductReview
    {
        public int ProductReviewId { get; set; }

        public int UserId { get; set; }
        public int ProductId { get; set; }

        public int Rating { get; set; } // ví dụ: 1-5
        public string Comment { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
