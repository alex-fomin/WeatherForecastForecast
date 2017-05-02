using System;
using System.Collections.Generic;

namespace WeatherFF.Loader.WeatherProviders.OpenWeatherMap.Dto
{
    public class List
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public Precipitation Snow { get; set; }
        public Precipitation Rain { get; set; }
        public Sys Sys { get; set; }
        public DateTime DtTxt { get; set; }
    }
}