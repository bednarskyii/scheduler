using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Scheduler.Models;
using Scheduler.Services.WeatherService;

namespace Scheduler.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private IWeatherService _weatherService;
        private readonly string _cityName = "Kyiv";
        private WeatherRoot items;

        private int temperature;
        private string city;
        private string weather;
        private string weatherUrl;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Temperature
        {
            get => temperature;
            set
            {
                 temperature = value;
                 PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Temperature)));
            }
        }
        public string City
        {
            get => city;
            set
            {
                city = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(City)));
            }
        }
        public string Weather
        {
            get => weather;
            set
            {
                weather = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Weather)));
            }
        }
        public string WeatherImageUrl
        {
            get => weatherUrl;
            set
            {
                weatherUrl = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(weatherUrl)));
            }
        }

        public WeatherViewModel()
        {
            _weatherService = new WeatherService();

            InitializeWeatherFields();
        }

        private async Task InitializeWeatherFields()
        {
            items = await _weatherService.GetCurrentWeather(_cityName);

            Temperature = Convert.ToInt32(items.MainWeather.Temperature);
            City = items.Name;
            Weather = items.Weather[0].Main;
            WeatherImageUrl = items.DisplayIcon;

            var weekWeather = await _weatherService.GetWeatherForWeek(_cityName);
        }

    }
}
