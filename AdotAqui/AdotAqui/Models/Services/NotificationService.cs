using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Notification Service used to send notifications
    /// </summary>
    public class NotificationService : INotificationService
    {
        /// <summary>
        /// Used to register a new notification, can send email
        /// </summary>
        /// <param name="context">AdotAquiDbContext</param>
        /// <param name="notification">Notification Details</param>
        /// <param name="emailService">Email Details (optional)</param>
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
