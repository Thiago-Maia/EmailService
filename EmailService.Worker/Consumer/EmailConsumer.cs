using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using EmailService.Domain;
using EmailService.Infra;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Worker.Consumer
{

    public class EmailConsumer : IConsumer<Email>
    {
        
        private readonly ILogger<EmailConsumer> _logger;
        private readonly AmazonSES _sesClient;

        public EmailConsumer(ILogger<EmailConsumer> logger, AmazonSES sesClient)
        {
            _logger = logger;
            _sesClient = sesClient;
        }

        public async Task Consume(ConsumeContext<Email> context)
        {
            var messageId = default(string);
            try
            {
                messageId = _sesClient.SendEmail((Email)context.Message).Result;
                _logger.LogInformation(messageId);
            }
            catch (Exception ex) 
            {
                _logger.LogInformation(ex.Message);
            }

        }
    }
}

