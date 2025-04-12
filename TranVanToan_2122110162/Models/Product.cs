namespace TranVanToan_2122110162.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }

        public double Price { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; } // ✅ Mô tả sản phẩm

        public Category Category { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        // 👉 Audit fields
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public int UserId { get; set; }
    }
}
