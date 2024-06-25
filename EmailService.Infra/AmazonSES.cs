using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using EmailService.Domain;

namespace EmailService.Infra
{
    public class AmazonSES
    {
        private readonly AmazonSimpleEmailServiceClient _sesClient;

        public AmazonSES(AmazonSimpleEmailServiceClient sesClient)
        {
            _sesClient = sesClient;
        }

        public async Task<string> SendEmail(Email email)
        {
            var emailReq = new SendEmailRequest
            {
                Destination = new Destination
                {
                    ToAddresses = new List<string> { email.To }
                },
                Message = new Message
                {
                    Body = new Body
                    {
                        Text = new Content { Charset = "UTF-8", Data = email.Body }
                    },
                    Subject = new Content
                    {
                        Charset = "UTF-8",
                        Data = email.Subject
                    }

                },
                Source = email.From
            };
            var response = await _sesClient.SendEmailAsync(emailReq);
            return response.MessageId;
        }
    }
}
