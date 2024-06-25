namespace EmailService.Domain
{
    public interface IEmail
    {
        string From { get; set; }
        string To { get; set; }
        string Body { get; set; }
        string Subject { get; set; }
    }
    public class Email
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
