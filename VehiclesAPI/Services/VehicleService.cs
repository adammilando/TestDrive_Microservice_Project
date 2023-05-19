using Microsoft.EntityFrameworkCore;
using VehiclesAPI.DbContexts;
using VehiclesAPI.Interfaces;
using VehiclesAPI.Models;

namespace VehiclesAPI.Services
{
    public class VehicleService : IVehicle
    {
        private ApiDbContext _dbContext;

        public VehicleService()
        {
            _dbContext = new ApiDbContext();
        }

        public async Task addVehicle(Vehicles vehicle)
        {
            await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteVehicle(int id)
        {
            var vehicle =  await _dbContext.Vehicles.FindAsync(id);
            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Vehicles>> GetAll()
        {
           var vehicles = await _dbContext.Vehicles.ToListAsync();
            return vehicles;
        }

        public async Task<Vehicles> GetById(int id)
        {
           var vehicle= await _dbContext.Vehicles.FindAsync(id);
            return vehicle;
        }

        public async Task updateVehicle(int id, Vehicles vehicle)
        {
            var vehicleUpdate = await _dbContext.Vehicles.FindAsync(id);
            vehicleUpdate.name = vehicle.name;
            vehicleUpdate.ImageUrl = vehicle.ImageUrl;
            vehicleUpdate.price = vehicle.price;
            vehicleUpdate.displacement = vehicle.displacement;
            vehicleUpdate.height = vehicle.height;
            vehicleUpdate.MaxSpeed = vehicle.MaxSpeed;
            vehicleUpdate.witdh = vehicle.witdh;

            await _dbContext.SaveChangesAsync();
        }
    }
}
