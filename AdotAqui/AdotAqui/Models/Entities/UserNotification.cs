using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdotAqui.Models.Entities
{
    public class UserNotification
    {
        [Key]
        public int UserNotificationId { get; set; }

        public DateTime NotificationDate { get; set; }
        public bool HasRead { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public int UserId { get; set; }
    }
}
