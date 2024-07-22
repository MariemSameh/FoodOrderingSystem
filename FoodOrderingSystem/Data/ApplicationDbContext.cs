using FoodOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<DeliveryMan> DeliveryMen { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
