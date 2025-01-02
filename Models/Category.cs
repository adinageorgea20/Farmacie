using System.ComponentModel.DataAnnotations;

namespace Farmacie.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } 

        public List<string> Products { get; set; } = new List<string>();
    }
}
