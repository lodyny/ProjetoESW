using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Mail;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AdotAquiContext _context;
        private readonly IEmailService _emailService;
        public SignUpService(AdotAquiContext context, IEmailService emailService, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _emailService = emailService;
            _contextAccessor = contextAccessor;
        }

        public async Task SignUpAsync(User user)
        {
            string activationKey = CreateActivationKey(user.Email);
            _context.Add(user);
            await _context.SaveChangesAsync();
            UserValidation userValidation = new UserValidation { UserID = user.UserID, ActivationKey = activationKey };
            _context.Add(userValidation);
            await _context.SaveChangesAsync();
            var to = new EmailAddress(user.Email, user.Name);
            var activationUrl = _contextAccessor.HttpContext.Request.Scheme + "://"
                                + _contextAccessor.HttpContext.Request.Host.ToUriComponent() + "/Users/Activate/" + activationKey;
            var subject = "AdotAqui - Ativação da conta";
            var plainTextContent = "Olá " + to.Name + "," + Environment.NewLine + "Seja bem-vindo ao AdotAqui. " +
                                   "Poderá finalizar o seu registo na plataforma através do seguint link: " + activationUrl + Environment.NewLine + Environment.NewLine +
                                   "Caso não tenha conhecimento do registo neste website ignore este email.";
            var htmlContent = "Olá " + to.Name + ",<br>Seja bem-vindo ao AdotAqui. " +
                              "Poderá finalizar o seu registo na plataforma através do seguint link: <a href='" + activationUrl + "'>" + activationUrl + "</a><br><br>" +
                              "Caso não tenha conhecimento do registo neste website ignore este email.";
            var from = new EmailAddress("noreply@adotaqui.com", "AdotAqui");
            Email activationEmail = new Email(from, to, subject, plainTextContent, htmlContent);
            await _emailService.SendAsync(activationEmail);
        }

        static private string CreateActivationKey(string email)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(email);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += string.Format("{0:x2}", x);
            }
            return hashString;
        }
    }
}
