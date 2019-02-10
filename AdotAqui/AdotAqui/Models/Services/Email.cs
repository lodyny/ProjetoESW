using SendGrid.Helpers.Mail;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Represents the email
    /// </summary>
    public class Email
    {
        public EmailAddress From { get; set; }
        public EmailAddress To { get; set; }
        public string Subject { get; set; }
        public string PlainBody { get; set; }
        public string HtmlBody { get; set; }

        /// <summary>
        /// Email Constructor
        /// </summary>
        /// <param name="from">From</param>
        /// <param name="to">To</param>
        /// <param name="subject">Subject</param>
        /// <param name="plainBody">Email Plain Body</param>
        /// <param name="htmlBody">Email HTML Body</param>
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
