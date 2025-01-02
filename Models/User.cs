using System.ComponentModel.DataAnnotations;

namespace Farmacie.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

       
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [RegularExpression(@"^[^@]+@[^@]+\.[^@]+$", ErrorMessage = "The email must contain '@' and a valid domain.")]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
