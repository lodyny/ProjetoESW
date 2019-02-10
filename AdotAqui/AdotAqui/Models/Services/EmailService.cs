using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Service used to send emails
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Sendgrid API Key
        /// </summary>
        private string SendGridKey { get; set; }

        /// <summary>
        /// EmailService Constructor
        /// </summary>
        /// <param name="iConfig">Configurations</param>
        public EmailService(IConfiguration iConfig)
        {
            SendGridKey = iConfig["SendGridKey"];
        }

        /// <summary>
        /// Sends the email async
        /// </summary>
        /// <param name="email">Email Class</param>
        /// <returns>Task</returns>
        public async Task SendAsync(Email email)
        {
            await ConfigSendGridAsync(email);
        }

        /// <summary>
        /// Sends the email async
        /// </summary>
        /// <param name="emailaddress">Email Address</param>
        /// <param name="subject">Subject</param>
        /// <param name="message">Message</param>
        /// <returns>Task</returns>
        public Task SendEmailAsync(string emailaddress, string subject, string message)
        {
            
            var from = new EmailAddress {Email = "noreply@adotaqui.com", Name = "AdotAqui"};
            var to = new EmailAddress {Email = emailaddress };
            var email = new Email(from, to, subject, message, message);
            return ConfigSendGridAsync(email);
        }

        /// <summary>
        /// Configs the sendgrid to send async
        /// </summary>
        /// <param name="email">Email Address</param>
        /// <returns>Task</returns>
        private async Task ConfigSendGridAsync(Email email)
        {
            var client = new SendGridClient(SendGridKey);
            var msg = MailHelper.CreateSingleEmail(email.From, email.To, email.Subject, email.PlainBody, email.HtmlBody);
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
