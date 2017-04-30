using System;

namespace WeatherFF.Common
{
    public class WeatherInfo
    {
        public string Provider { get; set; }
        public string City { get; set; }

        public DateTime Timestamp { get; set; }
        public DateTime ForecastTimestamp { get; set; }

        public int TemperatureHigh { get; set; }
        public int TemperatureLow { get; set; }

        public int Humidity { get; set; }
        public int Pressure { get; set; }

        public int WindSpeed { get; set; }
        public WindDirection WindDirection { get; set; }

        public SkyCondition Sky { get; set; }

        public int RainPer3Hour { get; set; }
        public int SnowPer3Hour { get; set; }
    }

    public enum SkyCondition
    {
        ClearSky = 0,
        FewClouds,
        ScatteredClouds,
        BrokenClouds,
        ShowerRain,
        Rain,
        Thunderstorm,
        Snow,
        Mist
    }

    public enum WindDirection
    {
       NW, N, NE,
        W,    E,
       SW, S, SE
    }
}