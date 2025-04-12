namespace TranVanToan_2122110162.Models
{
    public class ProductCreateDto
    {
        public string ProductName { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public int UserId { get; set; } // Người thao tác (thêm/sửa)
        public string Description { get; set; }

    }
}
