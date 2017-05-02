using System;
using WeatherFF.Common;
using WeatherFF.Loader.WeatherProviders.OpenWeatherMap;
using Xunit;

namespace WeatherFF.Tests
{
    public class UtilsTests
    {
        [Theory]
        [InlineData(-1.0, WindDirection.N)]
        [InlineData(0.0, WindDirection.N)]
        [InlineData(22.0, WindDirection.N)]
        [InlineData(23.0, WindDirection.NE)]
        [InlineData(45.0, WindDirection.NE)]
        [InlineData(90.0, WindDirection.E)]
        [InlineData(135.0, WindDirection.SE)]
        [InlineData(180.0, WindDirection.S)]
        [InlineData(225.0, WindDirection.SW)]
        [InlineData(270.0, WindDirection.W)]
        [InlineData(315.0, WindDirection.NW)]
        [InlineData(360.0, WindDirection.N)]
        [InlineData(362.0, WindDirection.N)]
        public void AngleToWindDirectionTest(double angle, WindDirection expectedDirection)
        {
            Assert.Equal(angle.AngleToWindDirection(), expectedDirection);
        }
    }
}