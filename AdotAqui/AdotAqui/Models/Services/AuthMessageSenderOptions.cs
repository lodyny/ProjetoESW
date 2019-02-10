using System;
using System.Linq;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Represents the authentication of sendgrid
    /// </summary>
    public class AuthMessageSenderOptions
    {
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
