using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Bluetooth : ContentPage
    {
        public Bluetooth()
        {
            InitializeComponent();
        }
        public class ShareTest
        {
            public async Task ShareText(string text)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = text,
                    Title = "Share Text"
                });
            }

            public async Task ShareUri(string uri)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Uri = uri,
                    Title = "Share Web Link"
                });
            }
        }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            if (bluetoothAdapter == null)
            {
                Toast.MakeText(this, "Bluetooth is not available", ToastLength.Long).Show();
                Finish();
                return;
            }
        }
    }

}