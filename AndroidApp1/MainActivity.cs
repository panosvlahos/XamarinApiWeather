using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidApp1.Models;
using AndroidApp1.Services;
using Nito.AsyncEx;
using Nito.AsyncEx.Synchronous;
using System;
using System.Reflection.Emit;
using Xamarin.Essentials;

using static Android.Views.View;
using Location = Xamarin.Essentials.Location;
using Service = AndroidApp1.Services.Service;

namespace AndroidApp1
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity 
    {
        //TextView? latitude;
        TextView? weather;
        Button? button;
        Service service = new Service();
        String? city;
        CurrentLocation.Root? response=new CurrentLocation.Root();
        //public EventHandler Button_Click { get; private set; }

        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            EditText edittext = FindViewById<EditText>(Resource.Id.edittext);
            weather = FindViewById<TextView>(Resource.Id.weather);

            button = FindViewById<Button>(Resource.Id.buttonId);
            if(button != null)
            //button.Click += OnButtonClicked;
            button.Click += async (object sender, EventArgs e) =>
            {
                city = edittext.Text.ToString();
                //var location=GetWeather(city);
                //var task = FindWeather(city);
                response = await service.GetCurrentLocation(city);
                if(response.weather == null )
                response = await service.GpsLocation();


                weather.Text = response.name.ToString();
                //var result = task.WaitAndUnwrapException();
                //var result = task.Result;
                //var result = AsyncContext.Run(GetWeather(city));
                //Console.WriteLine(location);
                //Toast.MakeText(this, city + "  ", ToastLength.Long).Show();

            };

        }
        //public async Task<CurrentLocation.Root?> FindWeather(string city)
        //{
        //    CurrentLocation.Root? location;
        //    location = await service.GetCurrentLocation(city);

        //    return location;
        //}
        //public async void OnMapReady(GoogleMap map)
        //{
        //    var location = await service.Location();

        //    map.UiSettings.ZoomControlsEnabled = true;
        //    map.UiSettings.CompassEnabled = true;
        //    MarkerOptions markerOpt1 = new MarkerOptions();
        //    markerOpt1.SetPosition(new LatLng(location.Latitude, location.Latitude));
        //    markerOpt1.SetTitle("Location");
        //    map.AddMarker(markerOpt1);

        //    GeoLocation? geoLocation = new GeoLocation();
        //    geoLocation.latitude = location.Latitude;
        //    geoLocation.longitude = location.Longitude;

        //    var descriptionLocation = await service.GetCurrentLocation(geoLocation);


        //    longitude = FindViewById<TextView>(Resource.Id.lon);
        //    latitude = FindViewById<TextView>(Resource.Id.lat);
        //    if (longitude != null && latitude != null)
        //    {
        //        latitude.Text = location.Latitude.ToString();
        //        longitude.Text = location.Longitude.ToString();
        //    }
        //}

        //private async void OnButtonClicked(object? sender, EventArgs e)
        //{
        //    Toast.MakeText(this, city + " " , ToastLength.Long).Show();
        //    //city = this.edittext.Text.ToString();
        //    //var mapFragment = (MapFragment)FragmentManager?.FindFragmentById(Resource.Id.map);
        //    //mapFragment?.GetMapAsync(this);


        //}
    }
}