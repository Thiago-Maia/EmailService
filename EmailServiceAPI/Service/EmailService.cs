using EmailService.Domain;
using MassTransit;

namespace EmailServiceAPI.Service
{
    public class EmailService
    {
        IBus _bus;
        public EmailService(IBus bus)
        {
            _bus = bus;
        }

        public async Task Save(Email email)
        {
            await _bus.Publish(email);

            return;
        }
    }
}
