using Microsoft.EntityFrameworkCore;
using ReservationAPI.Models;

namespace ReservationAPI.DbContexts
{
    public class ApiDbContext: DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectModels;Database=Reservation_db;");
        }
    }
}
