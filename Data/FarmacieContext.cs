using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Farmacie.Models;

namespace Farmacie.Data
{
    public class FarmacieContext : DbContext
    {
        public FarmacieContext(DbContextOptions<FarmacieContext> options)
            : base(options)
        {
        }

        public DbSet<Farmacie.Models.Category> Category { get; set; } = default!;
        public DbSet<Farmacie.Models.Product> Product { get; set; } = default!;
        public DbSet<Farmacie.Models.User> User { get; set; } = default!;

        public DbSet<Farmacie.Models.Order> Order { get; set; } = default!;
        public DbSet<Farmacie.Models.OrderDetail> OrderDetail { get; set; } = default!;


    }
}
