using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;


namespace App6
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
        }
        public static string username = "";
        public static string password = "";
        public  void Button_Clicked(object sender, EventArgs e)
        {
            //if (usrname.Text == null || passwrd.Text == null)
            //{
            //    DisplayAlert("Error", "Please enter your username or password.", "OK");
            //    return;
            //}
            if (usrname.Text == username && passwrd.Text == password)
            {
                Navigation.PushModalAsync(new Welcome());
            }
            else
            {
                DisplayAlert("Login Unsuccessful", "Your username or password is incorrect. Please check it and try again.", "OK");
                return;
            }
        }
        

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new SignUp());
    }

}
