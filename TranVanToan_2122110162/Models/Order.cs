using System.Text.Json.Serialization;

namespace TranVanToan_2122110162.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }

        // FK
        public int UserId { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
