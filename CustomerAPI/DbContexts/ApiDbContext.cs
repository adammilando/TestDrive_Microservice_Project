using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace CustomerAPI.DbContexts
{
    public class ApiDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=Customer_db;");
        }
    }
}
