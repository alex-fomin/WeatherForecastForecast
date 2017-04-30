using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using WeatherFF.Common;
using WeatherFF.Loader.WeatherProviders.OpenWeatherMap.Dto;

namespace WeatherFF.Loader.WeatherProviders.OpenWeatherMap
{
    public class OpenWeatherMapProvider : IWeatherProvider
    {
        private const string ApiKey = "59957517ecb25a59cfaa31eaa6fc770c";
        private readonly Uri _apiUri = new Uri("https://api.openweathermap.org");

        private readonly Dictionary<string, SkyCondition> _symbolsToConditions = new Dictionary<string, SkyCondition>
        {
            {"01d", SkyCondition.ClearSky},
            {"02d", SkyCondition.FewClouds},
            {"03d", SkyCondition.ScatteredClouds},
            {"04d", SkyCondition.BrokenClouds},
            {"09d", SkyCondition.ShowerRain},
            {"10d", SkyCondition.Rain},
            {"11d", SkyCondition.Thunderstorm},
            {"13d", SkyCondition.Snow},
            {"50d", SkyCondition.Mist},
            {"01n", SkyCondition.ClearSky},
            {"02n", SkyCondition.FewClouds},
            {"03n", SkyCondition.ScatteredClouds},
            {"04n", SkyCondition.BrokenClouds},
            {"09n", SkyCondition.ShowerRain},
            {"10n", SkyCondition.Rain},
            {"11n", SkyCondition.Thunderstorm},
            {"13n", SkyCondition.Snow},
            {"50n", SkyCondition.Mist},
        };

        private readonly Dictionary<string, int> _cityMap = new Dictionary<string, int>
        {
            {"Minsk, BY", 625144}
        };

        private readonly JsonSerializer _jsonSerializer;

        public OpenWeatherMapProvider()
        {
            _jsonSerializer = new JsonSerializer
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
        }

        public async Task<IEnumerable<WeatherInfo>> GetWeatherForecastAsync(string city)
        {
            using (var stream = await GetJsonStreamAsync(city))
            {
                var dto = _jsonSerializer.Deserialize<WeatherInfoDto>(new JsonTextReader(new StreamReader(stream)));
                return Transform(dto, city);
            }
        }

        private async Task<Stream> GetJsonStreamAsync(string city)
        {
            await Task.Yield();
            return File.OpenRead(@"/Users/sane/RiderProjects/WeatherForecastForecast/examples/openWeatherMap.json");
            /*
            var client = new HttpClient();
            return await client.GetStreamAsync(
                new Uri(_apiUri, $"data/2.5/forecast?id={MapCity(city)}&APPID={ApiKey}&units=metric"))
*/
        }

        private int MapCity(string city)
        {
            return _cityMap[city];
        }

        private IEnumerable<WeatherInfo> Transform(WeatherInfoDto data, string city)
        {
            return data.List.Select(x => new WeatherInfo
            {
                Provider = "OpenWeatherMap",
                City = city,
                ForecastTimestamp = x.DtTxt,
                Humidity = x.Main.Humidity,
                PercipitationType = x.Snow?.ThreeHours != null
                    ? PercipitationType.Snow
                    : x.Rain?.ThreeHours != null
                        ? PercipitationType.Rain
                        : PercipitationType.None,
                Precipitation = x.Snow?.ThreeHours ?? x.Rain?.ThreeHours,
                Pressure = x.Main.Pressure,
                Sky = DecodeSkyCondition(x.Weather.FirstOrDefault()),
                TemperatureHigh = x.Main.TempMax,
                TemperatureLow = x.Main.TempMin,
                WindSpeed = x.Wind.Speed,
                WindDirection = DecodeWindDirection(x.Wind.Deg),
                Timestamp = DateTime.UtcNow
            });
        }

        private WindDirection DecodeWindDirection(double windDeg)
        {
            const double step = 360 / 8.0;
            windDeg += step / 2;

            if (windDeg <= 1 * step) return WindDirection.N;
            if (windDeg <= 2 * step) return WindDirection.NE;
            if (windDeg <= 3 * step) return WindDirection.E;
            if (windDeg <= 4 * step) return WindDirection.SE;
            if (windDeg <= 5 * step) return WindDirection.S;
            if (windDeg <= 6 * step) return WindDirection.SW;
            if (windDeg <= 7 * step) return WindDirection.W;
            if (windDeg <= 8 * step) return WindDirection.NW;
            return WindDirection.N;
        }

        private SkyCondition DecodeSkyCondition(Weather weather)
        {
            return weather == null ? SkyCondition.Unknown : _symbolsToConditions.GetValueOrDefault(weather.Icon);
        }
    }
}