using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AdotAqui.Models.Services
{
    public class NotificationService : INotificationService
    {
        public void Register(AdotAquiDbContext context, UserNotification notification, IEmailSender emailService = null)
        {
            context.UserNotification.Add(notification);
            context.SaveChanges();

            if(emailService != null)
            {
                var user = context.Users.Find(notification.UserId);
                emailService.SendEmailAsync(user.Email, "Nova Notificação", "Olá, <p>Gostariamos de informar que tem uma nova notificação!</p><p>Visite <a href='http://eswapp.azurewebsites.net/'>Adotaqui</a> para mais detalhes</p>");
            }
        }
    }
}
