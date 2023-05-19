using Azure.Messaging.ServiceBus;
using CustomerAPI.DbContexts;
using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CustomerAPI.Services
{
    public class CustomerService : ICustomer
    {
        private ApiDbContext _dbContext;

        public CustomerService()
        {
            _dbContext = new ApiDbContext();
        }
        public async Task addCustomer(Customer customer)
        {
            var vehicleDb = await _dbContext.Vehicles.FirstOrDefaultAsync(options =>
            options.Id == customer.VehicleId);

            if(vehicleDb == null)
            {
                await _dbContext.Vehicles.AddAsync(customer.vehicle);
                await _dbContext.SaveChangesAsync();
            }

            customer.vehicle = null;
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();


            string connectionString = "Endpoint=sb://testdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=B8a+drSxt4kcUup8bu+nXHnCaV/WWYDO4+ASbKn/+xI=";
            string queueName = "testdrive";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);

            var objectCustomer = JsonConvert.SerializeObject(customer);

            // create the sender
            ServiceBusSender sender = client.CreateSender(queueName);

            // create a message that we can send. UTF-8 encoding is used when providing a string.
            ServiceBusMessage message = new ServiceBusMessage(objectCustomer);

            // send the message
            await sender.SendMessageAsync(message);

        }
    }
}
