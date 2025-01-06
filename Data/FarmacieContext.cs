using Farmacie.Models;
using Microsoft.EntityFrameworkCore;

namespace Farmacie.Data
{
    public class FarmacieContext : DbContext
    {
        public FarmacieContext(DbContextOptions<FarmacieContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderDetail> OrderDetail { get; set; } = default!;
    }
}
