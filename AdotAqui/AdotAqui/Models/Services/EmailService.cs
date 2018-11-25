using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Class Service used widely in the application to send emails
    /// </summary>
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        /// <summary>
        /// Constructor that receives the configuration
        /// </summary>
        /// <param name="iConfig"></param>
        /// <param name="optionsAccessor"></param>
        public EmailService(IConfiguration iConfig, IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
            config = iConfig;
        }

        /// <summary>
        /// Method that implement the interface method
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Task</returns>
        public async Task SendAsync(Email email)
        {
            await ConfigSendGridAsync(email);
        }

        /// <summary>
        /// Method used to send a e-mail async
        /// </summary>
        /// <param name="emailaddress">Destination's Email</param>
        /// <param name="subject">Email Subject</param>
        /// <param name="message">Email Message</param>
        /// <returns>Task</returns>
        public Task SendEmailAsync(string emailaddress, string subject, string message)
        {
            var from = new EmailAddress {Email = "noreply@adotaqui.com", Name = "AdotAqui"};
            var to = new EmailAddress {Email = emailaddress };
            var email = new Email(from, to, subject, message, message);
            return ConfigSendGridAsync(email);
        }

        /// <summary>
        /// Method used widely on the application to send emails to any user
        /// </summary>
        /// <param name="email">Email Class</param>
        /// <returns>Task</returns>
        private async Task ConfigSendGridAsync(Email email)
        {
            var client = new SendGridClient(Options.SendGridKey);
            var msg = MailHelper.CreateSingleEmail(email.From, email.To, email.Subject, email.PlainBody, email.HtmlBody);
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
