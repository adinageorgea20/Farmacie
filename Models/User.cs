using System.ComponentModel.DataAnnotations;

namespace Farmacie.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public string Role { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
