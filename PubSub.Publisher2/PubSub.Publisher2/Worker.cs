using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Rebus.Bus;
using Rebus.ServiceProvider;

namespace PubSub.Publisher2
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly IBus _bus;

        public Worker(IServiceProvider provider, IBus bus)
        {
            _provider = provider;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _provider.UseRebus();
            var count = 0;
            
            while (!stoppingToken.IsCancellationRequested)
            {
                count++;
                await Task.Delay(2000, stoppingToken);
                await _bus.Send($"Message #{count}");
            }
        }
    }
}