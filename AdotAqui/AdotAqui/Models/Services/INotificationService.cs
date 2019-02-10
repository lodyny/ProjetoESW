using AdotAqui.Data;
using AdotAqui.Models.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    /// <summary>
    /// Interface used to representate the notification service
    /// </summary>
    public interface INotificationService
    {
        void Register(AdotAquiDbContext context, UserNotification notification, IEmailSender emailService = null);
    }
}
