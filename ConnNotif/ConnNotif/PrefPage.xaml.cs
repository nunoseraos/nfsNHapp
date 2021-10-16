using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ConnNotif
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PrefPage : ContentPage
    {
        public PrefPage()
        {
            InitializeComponent();
            HubTagEntry.Text = Preferences.Get("HubTag", string.Empty);
            HubNameEntry.Text = Preferences.Get("HubName", string.Empty);
            HubConnStringEntry.Text = Preferences.Get("HubConnString", string.Empty);

        }

        private void SaveBttn_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("HubTag", HubTagEntry.Text);
            Preferences.Set("HubName", HubNameEntry.Text);
            Preferences.Set("HubConnString", HubConnStringEntry.Text);
            DisplayAlert("Sucesso", "Dados guardados com sucesso", "Ok");

        }
    }
}