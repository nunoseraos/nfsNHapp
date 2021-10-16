using System;
using Xamarin.Forms;

namespace ConnNotif
{
    public partial class App : Application
    {

        public App(bool shallNavigate)
        {
            InitializeComponent();
            if (shallNavigate)
            {
                //MainPage = new NavigationPage(new DashboardPage());
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }
        protected override void OnResume()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnStart()
        {
        }
    }
}