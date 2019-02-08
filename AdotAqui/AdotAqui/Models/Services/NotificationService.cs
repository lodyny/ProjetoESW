﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdotAqui.Data;
using AdotAqui.Models.Entities;

namespace AdotAqui.Models.Services
{
    public class NotificationService : INotificationService
    {
        public void Register(AdotAquiDbContext context, UserNotification notification)
        {
            context.UserNotification.Add(notification);
            context.SaveChanges();
        }
    }
}