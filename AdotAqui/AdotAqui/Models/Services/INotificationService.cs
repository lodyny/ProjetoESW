using AdotAqui.Data;
using AdotAqui.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Services
{
    public interface INotificationService
    {
        void Register(AdotAquiDbContext context, UserNotification notification, IEmailService emailService = null);
    }
}
