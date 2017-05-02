using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeatherFF.Common;

namespace WeatherFF.Loader.WeatherProviders.PogodaBy
{
    public class PogodaByProvider : IWeatherProvider
    {
        private readonly Dictionary<string, int> _cityMap = new Dictionary<string, int>()
        {
            {"Minsk, BY", 26850}
        };

        public async Task<IEnumerable<WeatherInfo>> GetWeatherForecastAsync(string city)
        {
            var cityId = MapCity(city);
            var client = new HttpClient();
            var body = client.GetStringAsync($"http://6.pogoda.by/{cityId}");
            //var r = new Regex
        //    CultureInfo
            return null;
        }

        /*
#r "System.Net.Http"
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Globalization;

var x = new HttpClient();
var body = x.GetStringAsync("http://6.pogoda.by/26850").Result;
var index = body.IndexOf("<tr>\n<td bgcolor='#F2F2FF' colspan='7'");
var index2 = body.IndexOf("</table>", index);
body = body.Substring(index, index2-index);
var parts = body.Split(new[]{"<tr>"}, StringSplitOptions.RemoveEmptyEntries);
var part=parts[1];

var datePart = Regex.Match(part, "<b>(.*?)</b>").Groups[1].Value;

var trs = part.Split(new[]{"<tr"}, StringSplitOptions.None).Skip(1).ToArray();
var tr=trs[1];

var timePart = Regex.Match(tr, "ночь|утро|день|вечер").Value;


var tempPart = Regex.Match(tr, "Tmin=([-+0-9.]+), Tmax=([-+0-9.]+)")

*/


        private int MapCity(string city)
        {
            return _cityMap[city];
        }
    }
}