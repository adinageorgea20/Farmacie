using System.ComponentModel.DataAnnotations;

namespace Farmacie.Models
{
    public class Category
    {
        public int ID { get; set; }
        [Required][StringLength(100)] public string Name { get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
