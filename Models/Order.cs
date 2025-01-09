using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmacie.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }

    }
}
