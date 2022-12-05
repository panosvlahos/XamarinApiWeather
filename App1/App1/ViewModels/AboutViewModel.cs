using App1.Models;
using App1.Services;
using Java.Security;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using static App1.Models.CurrentLocation;
using static System.Net.Mime.MediaTypeNames;


namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {


        Service service = new Service();
        private string text;
        private string countryname;
        private string countrytextlabel;
        private string cloudsText;
        private string cloudsTextlabel;
        private string speedtext;
        private string speedtextlabel;
        private string weathertext;
        private string weathertextlabel;
        //CurrentLocation.Root response = new CurrentLocation.Root();
        public Command WeatherButton { get; }
        public AboutViewModel()
        {
           
            Title = "About";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            WeatherButton = new Command(BtnContinue_Clicked);
        }
        
        public async void BtnContinue_Clicked(object obj)
        {
            
            var response = await service.GetCurrentLocation(text);
            
            if (response?.weather == null)
            response = await service.GpsLocation();

            countryName = response.name;
            clouds = response.clouds.all.ToString();
            weather = response.weather[0].description.ToString();
            speed = response.wind.speed.ToString();

            countryNameLabel = "City Name";
            cloudsLabel = "Clouds";
            speedLabel = "Speed";
            weatherLabel = "Weather";
        }
        public ICommand OpenWebCommand { get; }
        public ICommand GetWeather { get; }
        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }
        public string countryName
        {
            get => countryname;
            set => SetProperty(ref countryname, value);
        }
        public string countryNameLabel
        {
            get => countrytextlabel;
            set => SetProperty(ref countrytextlabel, value);
        }
        public string clouds
        {
            get => cloudsText;
            set => SetProperty(ref cloudsText, value);
        }
        public string cloudsLabel
        {
            get => cloudsTextlabel;
            set => SetProperty(ref cloudsTextlabel, value);
        }
        public string speedLabel
        {
            get => speedtextlabel;
            set => SetProperty(ref speedtextlabel, value);
        }
        public string speed
        {
            get => speedtext;
            set => SetProperty(ref speedtext, value);
        }
        public string weatherLabel
        {
            get => weathertextlabel;
            set => SetProperty(ref weathertextlabel, value);
        }
        public string weather
        {
            get => weathertext;
            set => SetProperty(ref weathertext, value);
        }
    }
}