using Android.App;
using ConnNotif.Model;
using WindowsAzure.Messaging;
using Xamarin.Forms;
using Xamarin.Essentials;
using Firebase.Messaging;
using Constants = ConnNotif.Model.Constants;


// https://firebase.google.com/docs/reference/android/com/google/firebase/messaging/FirebaseMessagingService
// https://firebase.google.com/docs/cloud-messaging/android/receive#sample-receive

namespace ConnNotif.Droid.Model
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FirebaseMessagingServiceEx : FirebaseMessagingService
    {
        private const string Template = "{\"notification\":{\"body\":\"$(body)\",\"title\":\"$(title)\"},\"data\":{\"Group_guid\":\"$(Group_guid)\",\"Group_name\":\"$(Group_name)\",\"Message\":\"$(Message)\",\"SendTimeStamp\":\"$(SendTimeStamp)\"}}";
        //private const string Template = "{\"group_guid\":\"$(group_guid)\",\"group_name\":\"$(group_name)\",\"message\":\"$(message)\",\"timestamp\":\"$(timestamp)\"}";

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);

            var receiver = DependencyService.Get<INotificationsReceiver>();

            var message = "Alerta enviado para " + remoteMessage.Data["Group_guid"];
            message += " (" + remoteMessage.Data["Group_guid"] + ")";
            message += " com timestamp de " + remoteMessage.Data["SendTimeStamp"] + ":";
            message += "\n".Replace("\n", System.Environment.NewLine) + remoteMessage.Data["Message"];
            receiver.RaiseNotificationReceived(message);
        }

        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            System.Diagnostics.Debug.WriteLine("TOKEN:");
            System.Diagnostics.Debug.WriteLine(token);

            var hub = new NotificationHub(
                Constants.HubName,
                Constants.HubConnectionString,
                MainActivity.Context);

            //var hub = new NotificationHub(
            //    Preferences.Get("HubName", "hubname"),
            //    Preferences.Get("HubConnString", "connection string"),
            //    MainActivity.Context);


            // register device with Azure Notification Hub using the token from FCM
            //var registration = hub.Register(token, Preferences.Get("HubTag", "hubtag"));
            var registration = hub.Register(token, Constants.HubTagName);

            // Register template
            var pnsHandle = registration.PNSHandle;
            var templateReg = hub.RegisterTemplate(
                pnsHandle,
                "defaultTemplate",
                Template,
                Constants.HubTagName);
                //Preferences.Get("HubTag", "hubtag"));

            var receiver = DependencyService.Get<INotificationsReceiver>();
            receiver.RaiseNotificationReceived("Pronto e registado...");

            System.Diagnostics.Debug.WriteLine("TEMPLATE:");
            System.Diagnostics.Debug.WriteLine(Template);

        }

    }
}