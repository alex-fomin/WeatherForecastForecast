namespace WeatherFF.Loader.WeatherProviders.OpenWeatherMap.Dto
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
    }
}