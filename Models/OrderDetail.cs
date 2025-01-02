using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmacie.Models
{
        public class OrderDetail
        {
            public int ID { get; set; }

            [Required]
            public int OrderID { get; set; }

            [Required]
            public int ProductID { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
            public int Quantity { get; set; }


            [ForeignKey("OrderID")]
            public virtual Order Order { get; set; }

            [ForeignKey("ProductID")]
            public virtual Product Product { get; set; }
        }
    }

