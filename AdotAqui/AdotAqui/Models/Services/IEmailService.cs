using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public interface IEmailService : IEmailSender
    {
        Task SendAsync(Email email);
    }
}
