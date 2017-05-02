using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Builder;
using Microsoft.Extensions.Logging;
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

            var container = BuildContainer();
            


            var providerName = args.FirstOrDefault() ?? "OpenWeatherMap";
            var city = args.Skip(1).FirstOrDefault() ?? "Minsk, BY";

            var provider = container.ResolveNamed<IWeatherProvider>(providerName);

            var info = provider.GetWeatherForecastAsync(city).Result;

            var logger = container.Resolve<ILogger>();

            logger.LogInformation(JsonConvert.SerializeObject(info, Formatting.Indented));
        }

        private static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new LoggingModule(f => f.AddConsole().AddDebug()));

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(x => x.IsAssignableTo<IWeatherProvider>())
                .Named<IWeatherProvider>(x => x.Name.Replace("Provider", ""));


            var container = builder.Build();
            return container;
        }


    }
}