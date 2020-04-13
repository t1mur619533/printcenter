using System.Collections.Generic;
using PrintCenter.Data.Models;

namespace PrintCenter.Domain.Notifications
{
    public class NotificationsEnvelope
    {
        public List<Notification> Notifications { get; set; }

        public NotificationsEnvelope(List<Notification> notifications)
        {
            Notifications = notifications;
        }
    }
}
