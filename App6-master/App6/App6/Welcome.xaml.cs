using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Welcome : ContentPage
    {
        public Welcome()
        {
            InitializeComponent();
            title1.Text = string.Format("Welcome, {0} !", MainPage.username);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Gmaps());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Bluetooth());
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            DisplayAlert("Error", "This feature is currently in development", "OK");
        }
    }
}