using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public class EmailService : IEmailService
    {
        private string SendGridKey { get; set; }

        public EmailService(IConfiguration iConfig)
        {
            SendGridKey = iConfig["SendGridKey"];
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
            var client = new SendGridClient(SendGridKey);
            var msg = MailHelper.CreateSingleEmail(email.From, email.To, email.Subject, email.PlainBody, email.HtmlBody);
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
