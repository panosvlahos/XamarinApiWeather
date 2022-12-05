using Android.Content.PM;
using Android.Gms.Common.Apis;
using Android.Util;
using AndroidApp1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AndroidApp1.Services
{

    public class Service
    {
        CurrentLocation.Root? items = new CurrentLocation.Root();
        GeoLocation? geoLocation = new GeoLocation();
        //public async Task<string> GetCurrentLocation(GeoLocation geoLocation,string city)
        public async Task<CurrentLocation.Root?> GetCurrentLocation(String city)
        {
            HttpClient client;
            client = new HttpClient();
            string appid = "09220a3eaa7b1b278c4be7d01aaf0e82";

            
            //var uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={geoLocation.latitude}&lon={geoLocation.longitude}&appid={appid}");

            var uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={appid}");
            items= await CallApi(uri);

            return items;
        }
        public async Task<CurrentLocation.Root?> GpsLocation()
        {
            Location location = new Location();
            string appid = "09220a3eaa7b1b278c4be7d01aaf0e82";
            try
            {
                location = await Geolocation.GetLocationAsync();
                //geoLocation.latitude = location.Latitude;
                if (location != null)
                {
                    //var bundle = PackageManager.GetApplicationInfo(apiWeatherKey, Android.Content.PM.PackageInfoFlags.MetaData).MetaData;
                    //foreach (var key in bundle.KeySet())
                    //{
                    //    Log.Debug("SO", $"{key} : {bundle.GetString(key)}");
                    //}
                    var uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&appid={appid}");
                    items = await CallApi(uri);
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Latitude}");
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Console.WriteLine($"error device not supported: {fnsEx}");
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Console.WriteLine($"error not enables on device exception: {fneEx}");
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                Console.WriteLine($"error not permission: {pEx}");
                // Handle permission exception
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error to get location: {ex}");
                // Unable to get location
            }
            return items;
        }
        private async Task<CurrentLocation.Root?> CallApi(System.Uri uri)
            {

            HttpClient myClient = new HttpClient();
            
            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<CurrentLocation.Root>(content);
            }
            return items;
        }
    }
}
