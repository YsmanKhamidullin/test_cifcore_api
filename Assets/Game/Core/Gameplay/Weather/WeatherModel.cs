using System;

namespace Game.Core.Gameplay.Weather
{
    public class WeatherModel
    {
        public string Name { get; set; } 
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public string Icon { get; set; }
    }
}