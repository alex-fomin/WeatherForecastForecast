using System.Collections.Generic;

namespace WeatherFF.Common
{
    public static class UtilsExtensions
    {
        public static double? ParseNullable(this string value)
        {
            if (double.TryParse(value, out var result))
                return result;
            else
                return null;
        }

        public static TValue GetValueOrDefault<T, TValue>(this Dictionary<T, TValue> dictionary, T key,
            TValue @default = default(TValue)) => dictionary.TryGetValue(key, out var value) ? value : @default;
    }
}