using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CustomerAPI.Interfaces;

namespace CustomerAPI.Services
{
    public class VehicleUpdateListenerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public VehicleUpdateListenerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomer>();
                await customerService.ListenForVehicleUpdates();
                await Task.Delay(1000, stoppingToken); // delay to prevent tight loop if there are no messages
            }
        }
    }
}
