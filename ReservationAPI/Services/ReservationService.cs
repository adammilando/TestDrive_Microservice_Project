using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ReservationAPI.DbContexts;
using ReservationAPI.Interfaces;
using ReservationAPI.Models;
using System.Net;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace ReservationAPI.Services
{
    public class ReservationService : IReservation
    {
        private ApiDbContext _dbContext;

        public ReservationService()
        {
            _dbContext = new ApiDbContext();
        }
        public async Task<List<Reservation>> GetAll()
        {
            string connectionString = "Endpoint=sb://testdrive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=B8a+drSxt4kcUup8bu+nXHnCaV/WWYDO4+ASbKn/+xI=";
            string queueName = "testdrive";
            // since ServiceBusClient implements IAsyncDisposable we create it with "await using"
            await using var client = new ServiceBusClient(connectionString);
            // create a receiver that we can use to receive the message
            ServiceBusReceiver receiver = client.CreateReceiver(queueName);

            // the received message is a different type as it contains some service set properties
            IReadOnlyList<ServiceBusReceivedMessage> receivedMessages = await receiver.ReceiveMessagesAsync(10);

            if (receivedMessages == null)
            {
                return null;
            }

            foreach (ServiceBusReceivedMessage receivedMessage in receivedMessages)
            {
                string body = receivedMessage.Body.ToString();
                var messageCreated = JsonConvert .DeserializeObject<Reservation>(body);
                await _dbContext.Reservations.AddAsync(messageCreated);
                await _dbContext.SaveChangesAsync();
                await receiver.CompleteMessageAsync(receivedMessage);
            }
            // get the message body as a string
            return await _dbContext.Reservations.ToListAsync();
        }

        public async Task UpdateMailStatus(int id)
        {
           var ReservationResult = await _dbContext.Reservations.FindAsync(id);
            if (ReservationResult != null && ReservationResult.IsMailSent == false)
            {
                ReservationResult.IsMailSent = true;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
