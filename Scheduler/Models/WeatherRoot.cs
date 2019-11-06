using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Scheduler.Models
{
    public class Weather
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [JsonProperty("main")]
        public string Main { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;
    }

    public class Main
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; } = 0;
        [JsonProperty("temp_min")]
        public double MinTemperature { get; set; } = 0;
        [JsonProperty("temp_max")]
        public double MaxTemperature { get; set; } = 0;
    }

    public class WeatherRoot
    {

        [JsonProperty("weather")]
        public List<Weather> Weather { get; set; } = new List<Weather>();

        [JsonProperty("main")]
        public Main MainWeather { get; set; } = new Main();

        [JsonProperty("id")]
        public int CityId { get; set; } = 0;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("dt_txt")]
        public string Date { get; set; } = string.Empty;


        [JsonIgnore]
        public string DisplayIcon => $"http://openweathermap.org/img/w/{Weather?[0]?.Icon}.png";
    }

}
