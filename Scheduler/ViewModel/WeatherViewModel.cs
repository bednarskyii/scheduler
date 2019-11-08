using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Scheduler.Models;
using Scheduler.Services.WeatherService;
using System.Collections.ObjectModel;

namespace Scheduler.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private IWeatherService _weatherService;
        private readonly string _cityName = "Kyiv";
        private WeatherRoot items;
        private ObservableCollection<SmallWeatherDisplay> _mainWeatherList;

        private int temperature;
        private string city;
        private string weather;
        private string weatherUrl;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<SmallWeatherDisplay> MainWeatherList
        {
            get
            {
                return _mainWeatherList;
            }
            set
            {
                _mainWeatherList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainWeatherList)));
            }
        }


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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WeatherImageUrl)));
            }
        }

        public WeatherViewModel()
        {
            _mainWeatherList = new ObservableCollection<SmallWeatherDisplay>();
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

            WeekWeather.RootObject weekWeather = await _weatherService.GetWeatherForWeek(_cityName);

            List<WeekWeather.List> wideList = weekWeather.list;

            DateTime currentDate = DateTime.Today;

            foreach (WeekWeather.List item in wideList)
            {
                if(Convert.ToDateTime(item.dt_txt).Date == currentDate.Date)
                {
                    _mainWeatherList.Add(new SmallWeatherDisplay
                    {
                        Temp = Convert.ToInt32(item.main.temp).ToString() + "°",
                        Date = Convert.ToDateTime(item.dt_txt),
                        WeatherUrl = $"http://openweathermap.org/img/w/{item.weather[0].icon}.png",
                        Day = Convert.ToDateTime(item.dt_txt).DayOfWeek
                    });

                    currentDate = currentDate.AddDays(1);
                }
            }
            
        }

    }
}
