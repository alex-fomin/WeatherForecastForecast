using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace WeatherFF.Common.Store
{
    public interface IStore
    {
        void SaveWeatherInfo(WeatherInfo info);
    }

    class Store : IStore
    {
        private readonly IMongoCollection<WeatherInfo> _weatherInfoCollection;

        static Store()
        {
            BsonClassMap.RegisterClassMap<WeatherInfo>();
        }

        public Store(IMongoClient client)
        {
            var database = client.GetDatabase("WeatherFF");
            _weatherInfoCollection = database.GetCollection<WeatherInfo>("WeatherInfo");
        }

        public void SaveWeatherInfo(WeatherInfo info)
        {
            _weatherInfoCollection.InsertOne(info);
        }
    }
}