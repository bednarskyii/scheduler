using System;
using System.Threading.Tasks;
using Scheduler.Models;
using static Scheduler.Models.WeekWeather;

namespace Scheduler.Services.WeatherService
{
    public interface IWeatherService
    {
        Task<WeatherRoot> GetCurrentWeather(string city);
        Task<WeekWeather.RootObject> GetWeatherForWeek(string city);
    }
}
