using System;

namespace ConnNotif.Model
{
    public interface INotificationsReceiver
    {
        event EventHandler<string> ErrorReceived;

        event EventHandler<string> NotificationReceived;

        void RaiseErrorReceived(string message);

        void RaiseNotificationReceived(string message);
    }
}