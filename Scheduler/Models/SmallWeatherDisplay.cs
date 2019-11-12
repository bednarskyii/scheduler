using System;

namespace Scheduler.Models
{
    public class SmallWeatherDisplay
    {
        public string Temp { get; set; }
        public string WeatherUrl { get; set; }
        public DayOfWeek Day { get; set; }
        public DateTime Date { get; set; }
    }
}
