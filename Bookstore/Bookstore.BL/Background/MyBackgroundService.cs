using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Bookstore.BL.BackgroundServices
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private int executionCount = 0;

        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        } 

        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            Task task = Task.Run(async () =>
            {
                using PeriodicTimer time = new PeriodicTimer(TimeSpan.FromSeconds(2));
                while (!cancellationToken.IsCancellationRequested && await time.WaitForNextTickAsync(cancellationToken))
                {
                    _logger.LogInformation($"Starting background service: {nameof(MyBackgroundService)}");
                }
            });

            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Stopping background service: {nameof(MyBackgroundService)}");

            return Task.CompletedTask;
        }

        private void Action(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation($"Timed Hosted Service is working. Count: {count}", count);
        }
    }
}
