using VehiclesAPI.Models;

namespace VehiclesAPI.Interfaces
{
    public interface IVehicle
    {
       Task<List<Vehicles>> GetAll();
        Task<Vehicles> GetById(int id);
        Task addVehicle(Vehicles vehicle);
        Task updateVehicle(int id, Vehicles vehicle);
        Task deleteVehicle(int id);
    }
}
