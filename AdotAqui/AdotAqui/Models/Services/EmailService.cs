using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration config;

        public EmailService(IConfiguration iConfig)
        {
            config = iConfig;
        }

        public async Task SendAsync(Email email)
        {
            await ConfigSendGridAsync(email);
        }

        private async Task ConfigSendGridAsync(Email email)
        {
            var client = new SendGridClient(config.GetValue<string>("EmailAPIKey"));
            var msg = MailHelper.CreateSingleEmail(email.From, email.To, email.Subject, email.PlainBody, email.HtmlBody);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
