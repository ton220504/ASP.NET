namespace TranVanToan_2122110162.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        // Optional: navigation
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
