using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherFF.Common;

namespace WeatherFF.Loader.WeatherProviders
{
    public interface IWeatherProvider
    {
        Task<IEnumerable<WeatherInfo>> GetWeatherForecastAsync(string city);
    }
}