using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public ICustomer _customerService;

        public CustomerController(ICustomer customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            var vehilce = await _customerService.GetAllCustomers();
            return vehilce;
        }

        [HttpPost]
        public async Task create([FromBody] Customer customer)
        {
            await _customerService.addCustomer(customer);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _customerService.DeleteCustomer(id);
        }

    }
}
