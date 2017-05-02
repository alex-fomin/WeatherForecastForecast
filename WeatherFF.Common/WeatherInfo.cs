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

        public double? Precipitation3Hours { get; set; }
        public PercipitationType PercipitationType { get; set; }

        public string RawInfo { get; set; }
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
        RainSnow,
        Mist
    }

    public enum WindDirection
    {
       NW, N, NE,
        W,    E,
       SW, S, SE
    }

    public static class WindDirectionExtension
    {
        public static WindDirection AngleToWindDirection(this double windDeg)
        {
            if (windDeg >= 360)
                windDeg = windDeg - 360;
            if (windDeg < 0)
                windDeg += 360;

            const double step = 360 / 8.0;
            windDeg += step / 2;

            if (windDeg < 1 * step) return WindDirection.N;
            if (windDeg < 2 * step) return WindDirection.NE;
            if (windDeg < 3 * step) return WindDirection.E;
            if (windDeg < 4 * step) return WindDirection.SE;
            if (windDeg < 5 * step) return WindDirection.S;
            if (windDeg < 6 * step) return WindDirection.SW;
            if (windDeg < 7 * step) return WindDirection.W;
            if (windDeg < 8 * step) return WindDirection.NW;
            return WindDirection.N;
        }
    }
}