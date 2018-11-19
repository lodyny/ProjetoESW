using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public EmailService(IConfiguration iConfig, IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
            config = iConfig;
        }

        public async Task SendAsync(Email email)
        {
            await ConfigSendGridAsync(email);
        }

        public Task SendEmailAsync(string emailaddress, string subject, string message)
        {
            var from = new EmailAddress {Email = "noreply@adotaqui.com", Name = "AdotAqui"};
            var to = new EmailAddress {Email = emailaddress };
            var email = new Email(from, to, subject, message, message);
            return ConfigSendGridAsync(email);
        }

        private async Task ConfigSendGridAsync(Email email)
        {
            var client = new SendGridClient(Options.SendGridKey);
            var msg = MailHelper.CreateSingleEmail(email.From, email.To, email.Subject, email.PlainBody, email.HtmlBody);
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
