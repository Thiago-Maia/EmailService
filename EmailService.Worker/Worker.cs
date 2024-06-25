using EmailService.Domain;
using MassTransit;

namespace EmailService.Worker
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
            while (!stoppingToken.IsCancellationRequested)
            {
                //await _bus.Publish(new Email { From = $"The time is {DateTimeOffset.Now}" });

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
