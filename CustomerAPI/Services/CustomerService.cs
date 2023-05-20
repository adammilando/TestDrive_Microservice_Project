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
        public async Task ListenForVehicleUpdates()
        {
            string connectionString = "Endpoint=sb://testdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WKLhJ1o0Za/THFeh22ILgDV25cT9D9KTT+ASbE0f8y8=";
            string queueName = "vehicleinfo";
            await using var client = new ServiceBusClient(connectionString);
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);

            foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
            {
                string body = receivedMessage.Body.ToString();
                var vehicle = JsonConvert.DeserializeObject<Vehicle>(body);

                var vehicleDb = await _dbContext.Vehicles.FirstOrDefaultAsync(options => options.Id == vehicle.Id);

                if (vehicleDb == null)
                {
                    await _dbContext.Vehicles.AddAsync(vehicle);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    // update the vehicle data in the Customer service
                    // you may also want to check if the vehicle was deleted
                    vehicleDb.Name = vehicle.Name;
                    await _dbContext.SaveChangesAsync();
                }

                await receiver.CompleteMessageAsync(receivedMessage);
            }
        }

        public async Task addCustomer(Customer customer)
        {
            var vehicleDb = await _dbContext.Vehicles.FirstOrDefaultAsync(options =>
            options.Id == customer.VehicleId);

            if (vehicleDb != null)
            {
                customer.vehicle = vehicleDb;
            }
            else
            {
                throw new Exception("Vehicle not found");

            }

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();


            string connectionString = "Endpoint=sb://testdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=B8a+drSxt4kcUup8bu+nXHnCaV/WWYDO4+ASbKn/+xI=";
            string queueName = "testdriveinfo";
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

        public async Task<Customer> GetCustomer(int id)
        {
             var customer = await _dbContext.Customers
                .Include(c => c.vehicle)
                .FirstOrDefaultAsync(c => c.Id == id);
            return customer;
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null)
            {
                throw new Exception("Customer not found");
            }

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            // Send a message to Service Bus for the deletion
            string connectionString = "Endpoint=sb://testdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=B8a+drSxt4kcUup8bu+nXHnCaV/WWYDO4+ASbKn/+xI=";
            string queueName = "testdriveinfo";
            await using var client = new ServiceBusClient(connectionString);

            var objectCustomer = JsonConvert.SerializeObject(customer);

            ServiceBusSender sender = client.CreateSender(queueName);

            ServiceBusMessage message = new ServiceBusMessage(objectCustomer);
            message.ApplicationProperties.Add("DeleteOperation", true);

            await sender.SendMessageAsync(message);
        }


        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = await _dbContext.Customers
                .Include(c => c.vehicle) 
                .ToListAsync();
            return customers;
        }


    }
}
