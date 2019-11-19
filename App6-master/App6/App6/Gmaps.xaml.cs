using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;

namespace App6
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    public partial class Gmaps : ContentPage
    {
        public Gmaps()
        {
            InitializeComponent();
            UpdateMap();
            Task.Factory.StartNew(CheckLocationPermission);
        }


        List<Place> placesList = new List<Place>();

        private async void UpdateMap()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Gmaps)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("App6.Places.json");
                string text = string.Empty;
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }

                var resultObject = JsonConvert.DeserializeObject<Places>(text);

                foreach (var place in resultObject.results)
                {
                    placesList.Add(new Place
                    {
                        PlaceName = place.name,
                        Address = place.vicinity,
                        Location = place.geometry.location,
                        Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
                        //Icon = place.icon,
                        //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                        //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                    });
                }

                MyMap.ItemsSource = placesList;
                //PlacesListView.ItemsSource = placesList;
                //var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-36.905807, 174.684850), Distance.FromKilometers(0.3)));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        private async void CheckLocationPermission()
        {
            PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
            if (status != PermissionStatus.Granted)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        //await DisplayAlert("Need location Permission", "Location permission not available", "OK");
                    }
                });


                Device.BeginInvokeOnMainThread(async () =>
                {
                    var result = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    status = result.FirstOrDefault().Value;
                });
            }

            if (status == PermissionStatus.Granted)
            {
                // turn on location
                DependencyService.Get<ILocation>().turnOnGps();
            }
            else if (status != PermissionStatus.Unknown)
            {
                //location denied
            }
        }
        private async void MoveToCurrentLocation()
        {
            CurrentPosition position = await GetPosition();

            Position mapPosition = new Position(position.Latitude, position.Longitude);

            Device.BeginInvokeOnMainThread(() => {

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(mapPosition, Distance.FromMiles(3)));

                var mapPin = new Pin
                {
                    Type = PinType.Place,
                    Position = mapPosition,
                    Label = "Coral&Hiuson",
                    
                };

                MyMap.Pins.Add(mapPin);

            });
        }
        private async Task<CurrentPosition> GetPosition()
        {
            CurrentPosition p = new CurrentPosition();
            if (CrossGeolocator.Current.IsGeolocationAvailable)
            {
                if (CrossGeolocator.Current.IsGeolocationEnabled)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;

                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

                    p.Latitude = position.Latitude;
                    p.Longitude = position.Longitude;

                }
                else
                {
                    await DisplayAlert("Message", "GPS Not Enabled", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Message", "GPS Not Available", "Ok");
            }
            return p;
        }
        private void MoveToCurrentlocation(object sender, EventArgs e)
        {
            MoveToCurrentLocation();
        }
    }

    public class CurrentPosition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

}

    
}
