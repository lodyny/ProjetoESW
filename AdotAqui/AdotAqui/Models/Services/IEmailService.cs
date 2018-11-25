using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public interface IEmailService : IEmailSender
    {
        Task SendAsync(Email email);
    }
}
