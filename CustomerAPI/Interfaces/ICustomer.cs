using CustomerAPI.Models;

namespace CustomerAPI.Interfaces
{
    public interface ICustomer
    {
        Task ListenForVehicleUpdates();
        Task addCustomer(Customer customer);
        Task<Customer> GetCustomer(int id);
        Task DeleteCustomer(int id);
        Task<List<Customer>> GetAllCustomers();

    }
}
