using System.Text.Json.Serialization;

namespace TranVanToan_2122110162.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; }
    }
}
