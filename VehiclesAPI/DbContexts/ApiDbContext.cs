using Microsoft.EntityFrameworkCore;
using VehiclesAPI.Models;

namespace VehiclesAPI.DbContexts
{
    public class ApiDbContext: DbContext
    {
        public DbSet<Vehicles> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=Vehicle_db;");
        }
    }
}
