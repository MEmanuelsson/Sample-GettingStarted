using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace GettingStarted
{
    public class Worker : BackgroundService
    {
        readonly IBus _bus;

        public Worker(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var filters = new List<KeyValuePair<string, string>>
            {
                new("Key1", "Value1"),
                new("Key2", "Value2")
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                await _bus.Publish(new Message
                {
                    Text = $"The time is {DateTimeOffset.Now}",
                    Filters = filters
                }, stoppingToken);

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}