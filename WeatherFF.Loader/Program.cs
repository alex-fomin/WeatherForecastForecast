using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WeatherFF.Common;
using WeatherFF.Loader.WeatherProviders;

namespace WeatherFF.Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = {new StringEnumConverter()}
            };

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(x => x.IsAssignableTo<IWeatherProvider>())
                .Named<IWeatherProvider>(x => x.Name.Replace("Provider", ""));

            var container = builder.Build();

            var providerName = args.FirstOrDefault() ?? "OpenWeatherMap";
            var city = args.Skip(1).FirstOrDefault() ?? "Minsk, BY";

            var provider = container.ResolveNamed<IWeatherProvider>(providerName);

            var info = provider.GetWeatherForecastAsync(city).Result;

            Console.WriteLine(JsonConvert.SerializeObject(info, Formatting.Indented));
        }
    }
}