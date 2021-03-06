using ConnNotif.Model;
using System;

#if DROID
[assembly: Xamarin.Forms.Dependency(typeof(ConnNotif.Droid.Model.NotificationsReceiver))]
#endif

namespace ConnNotif.Droid.Model
{
    public class NotificationsReceiver : INotificationsReceiver
    {
        public event EventHandler<string> ErrorReceived;

        public event EventHandler<string> NotificationReceived;

        public void RaiseErrorReceived(string message)
        {
            ErrorReceived?.Invoke(this, message);
        }

        public void RaiseNotificationReceived(string message)
        {
            NotificationReceived?.Invoke(this, message);
        }
    }
}