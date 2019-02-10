using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Interface used to representate Email Service
    /// </summary>
    public interface IEmailService : IEmailSender
    {
        Task SendAsync(Email email);
    }
}
