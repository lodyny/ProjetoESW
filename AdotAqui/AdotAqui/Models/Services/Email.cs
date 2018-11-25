using SendGrid.Helpers.Mail;

namespace AdotAqui.Models.Services
{
    public class Email
    {
        public EmailAddress From { get; set; }
        public EmailAddress To { get; set; }
        public string Subject { get; set; }
        public string PlainBody { get; set; }
        public string HtmlBody { get; set; }

        public Email(EmailAddress from, EmailAddress to, string subject, string plainBody, string htmlBody)
        {
            From = from;
            To = to;
            Subject = subject;
            PlainBody = plainBody;
            HtmlBody = htmlBody;
        }
    }
}
