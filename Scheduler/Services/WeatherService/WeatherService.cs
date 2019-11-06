using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Scheduler.Models;

namespace Scheduler.Services.WeatherService
{
    public class WeatherService : IWeatherService
    {
        public async Task<WeatherRoot> GetCurrentWeather(string city)
        {
            WeatherRoot currentWeather = null;

            using (HttpClient _client = new HttpClient())
            {
                string requestUrl = string.Format($"http://api.openweathermap.org/data/2.5/weather?appid=9a324a6b139e0715c71faf3dba25b1b5&units=metric&q={city}");

                var response = await _client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    currentWeather = JsonConvert.DeserializeObject<WeatherRoot>(content);
                }
            }
            return currentWeather;
        }

        public async Task<WeekWeather.RootObject> GetWeatherForWeek(string city)
        {
            WeekWeather.RootObject currentWeather = null;

            using (HttpClient _client = new HttpClient())
            {
                string requestUrl = string.Format($"http://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid=9a324a6b139e0715c71faf3dba25b1b5");

                var response = await _client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    currentWeather = JsonConvert.DeserializeObject<WeekWeather.RootObject>(content);
                }
            }
            return currentWeather;
        }
    }
}
