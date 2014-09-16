using System;
using System.Linq;
using Forecast.io.Entities;

namespace Forecast.io.Helpers
{
    public static class RequestHelpers
    {
        public static string FormatResponse(string input)
        {
            input = input.Replace("isd-stations", "isd_stations");
            input = input.Replace("lamp-stations", "lamp_stations");
            input = input.Replace("metar-stations", "metar_stations");
            input = input.Replace("darksky-stations", "darksky_stations");
            return input;
        }

        public static string FormatExcludeString(Exclude[] input)
        {
            return string.Join(",", input.Select(i => Enum.GetName(typeof(Exclude), i)));
        }

        public static string FormatExtendString(Extend[] input)
        {
            return string.Join(",", input.Select(i => Enum.GetName(typeof(Extend), i)));
        }
    }
}
