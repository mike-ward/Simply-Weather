using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Forecast.io.Extensions;
using Forecast.io.Helpers;

namespace Forecast.io.Entities
{
    public class ForecastIORequest
    {
        private readonly string _apiKey;
        private readonly string _latitude;
        private readonly string _longitude;
        private readonly string _unit;
        private readonly string _exclude;
        private readonly string _extend;
        private readonly string _time;

        private const string CurrentForecastUrl = "https://api.forecast.io/forecast/{0}/{1},{2}?units={3}&extend={4}&exclude={5}";
        private const string PeriodForecastUrl = "https://api.forecast.io/forecast/{0}/{1},{2},{3}?units={4}&extend={5}&exclude={6}";

        public ForecastIORequest(string apiKey, float latF, float longF, Unit unit, Extend[] extend = null, Exclude[] exclude = null)
        {
            _apiKey = apiKey;
            _latitude = latF.ToString(CultureInfo.InvariantCulture);
            _longitude = longF.ToString(CultureInfo.InvariantCulture);
            _unit = Enum.GetName(typeof(Unit), unit);
            _extend = (extend != null) ? RequestHelpers.FormatExtendString(extend) : "";
            _exclude = (exclude != null) ? RequestHelpers.FormatExcludeString(exclude) : "";
        }

        public ForecastIORequest(string apiKey, float latF, float longF, DateTime time, Unit unit, Extend[] extend = null, Exclude[] exclude = null)
        {
            _apiKey = apiKey;
            _latitude = latF.ToString(CultureInfo.InvariantCulture);
            _longitude = longF.ToString(CultureInfo.InvariantCulture);
            _time = time.ToUtcString();
            _unit = Enum.GetName(typeof(Unit), unit);
            _extend = (extend != null) ? RequestHelpers.FormatExtendString(extend) : "";
            _exclude = (exclude != null) ? RequestHelpers.FormatExcludeString(exclude) : "";
        }

        public async Task<ForecastIOResponse> Get()
        {
            var url = (_time == null) 
                ? String.Format(CurrentForecastUrl, _apiKey, _latitude, _longitude, _unit, _extend, _exclude) 
                : String.Format(PeriodForecastUrl, _apiKey, _latitude, _longitude, _time, _unit, _extend, _exclude);

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var json = await httpClient.GetStringAsync(url);
                    json = RequestHelpers.FormatResponse(json);
                    var serializer = new JavaScriptSerializer();
                    var forecast = serializer.Deserialize<ForecastIOResponse>(json);
                    return forecast;
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
    }
}
