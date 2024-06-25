using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using EmailService.Domain;
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
        private readonly AmazonSimpleEmailServiceClient _sesClient;
        private readonly ILogger<EmailConsumer> _logger;

        public EmailConsumer(ILogger<EmailConsumer> logger, AmazonSimpleEmailServiceClient sesClient)
        {
            _logger = logger;
            _sesClient = sesClient;
        }

        public async Task Consume(ConsumeContext<Email> context)
        {
            _logger.LogInformation(
                $"De: {context.Message.From} |" +
                $" Para: {context.Message.To} |" +
                $" Assunto: {context.Message.Subject} | " +
                $"Mensagem: {context.Message.Body}");
            var messageId = default(string);
            try
            {
                var emailReq = new SendEmailRequest
                {
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { context.Message.To }
                    },
                    Message = new Message
                    {
                        Body = new Body
                        {
                            Text = new Content { Charset = "UTF-8", Data = context.Message.Body }
                        },
                        Subject = new Content
                        {
                            Charset = "UTF-8",
                            Data = context.Message.Subject
                        }

                    },
                    Source = context.Message.From
                };
                var response = await _sesClient.SendEmailAsync(emailReq);
                messageId = response.MessageId;
            }
            catch (Exception ex) 
            {
                _logger.LogInformation(ex.Message);
            }

        }
    }
}

