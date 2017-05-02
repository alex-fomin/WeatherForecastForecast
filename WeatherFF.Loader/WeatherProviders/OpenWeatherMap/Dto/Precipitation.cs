using Newtonsoft.Json;

namespace WeatherFF.Loader.WeatherProviders.OpenWeatherMap.Dto
{
    public class Precipitation
    {
        [JsonProperty("3h")]
        public double? ThreeHours { get; set; }
    }
}