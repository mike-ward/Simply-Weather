using System;
using System.Globalization;

namespace Forecast.io.Extensions
{
    public static class Extensions
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime ToDateTime(this Int64 input)
        {
            return UnixEpoch.AddSeconds(input);
        }

        public static string ToUtcString(this DateTime input)
        {
            var milliseconds = input.ToUniversalTime().Subtract(UnixEpoch).TotalSeconds;
            return Convert.ToInt64(milliseconds).ToString(CultureInfo.InvariantCulture);
        }
    }
}
