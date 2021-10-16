using Android.App;
using Android.Content.PM;
using Android.Gms.Common;
using Android.OS;
using Android.Runtime;
using ConnNotif.Model;
using System.IO;
using Xamarin.Forms;
using SQLite;
using Android.Content;
using WindowsAzure.Messaging;
using Xamarin.Essentials;

namespace ConnNotif.Droid
{
    [Activity(Label = "ConnNotif", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const string ChannelId = "ConnNotif.Channel";

        public static MainActivity Context
        {
            get;
            private set;
        }

        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                // Notification channels are new in API 26 (and not a part of the
                // support library). There is no need to create a notification
                // channel on older versions of Android.
                return;
            }

            var channel = new NotificationChannel(
                ChannelId,
                "Notifications for Connect@fashion",
                NotificationImportance.Default);

            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            notificationManager.CreateNotificationChannel(channel);
        }

        private bool IsPlayServicesAvailable(INotificationsReceiver receiver)
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);

            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    receiver.RaiseErrorReceived(GoogleApiAvailability.Instance.GetErrorString(resultCode));
                }
                else
                {
                    receiver.RaiseErrorReceived("Dispositivo não suportado");
                    Finish();
                }
                return false;
            }
            else
            {
                receiver.RaiseNotificationReceived("Os serviços Google Play estão disponíveis.");
                return true;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Context = this;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //ini:database_create - abandonado - pode servir para guardar histórico de notificações recebidas
            //string dbName= "db-connfashion1";
            //string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            //string fullPath = Path.Combine(folderPath, dbName);
            //LoadApplication(new App(fullPath));
            //fim:database

            bool flag = false;
            if (Intent.Extras != null)
            {
                flag = true;
            }

            LoadApplication(new App(flag));


            var receiver = DependencyService.Get<INotificationsReceiver>();
            receiver.RaiseNotificationReceived("A registar...");

            if (IsPlayServicesAvailable(receiver))
            {
                CreateNotificationChannel();
                receiver.RaiseNotificationReceived("Pronto...");
            }

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }




    }
}