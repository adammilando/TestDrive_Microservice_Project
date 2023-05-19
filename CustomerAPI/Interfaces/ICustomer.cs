using CustomerAPI.Models;

namespace CustomerAPI.Interfaces
{
    public interface ICustomer
    {
        Task addCustomer(Customer customer);
         
    }
}
