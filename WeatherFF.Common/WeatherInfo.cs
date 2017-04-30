using System;

namespace WeatherFF.Common
{
    public class WeatherInfo
    {
        public string Provider { get; set; }

        public string City { get; set; }


        public DateTime Timestamp { get; set; }
        public DateTime ForecastTimestamp { get; set; }

        public double TemperatureHigh { get; set; }
        public double TemperatureLow { get; set; }

        public int Humidity { get; set; }
        public double Pressure { get; set; }

        public double WindSpeed { get; set; }
        public WindDirection WindDirection { get; set; }

        public SkyCondition Sky { get; set; }

        public double? Precipitation { get; set; }
        public PercipitationType PercipitationType { get; set; }
    }

    public enum PercipitationType
    {
        None = 0,
        Rain,
        Snow
    }

    public enum SkyCondition
    {
        Unknown = 0,
        ClearSky,
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