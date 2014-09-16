using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Forecast.io.Entities;
using Forecast.io.Extensions;

namespace SimplyWeather.Models
{
    public class ForcastIoDataProvider : IWeatherDataProvider
    {
        public async Task<WeatherData> GetWeather()
        {
            var settings = Properties.Settings.Default;
            var units = settings.Celsius ? Unit.ca : Unit.us;
            var forecast = new ForecastIORequest(settings.ApiKey, settings.Latitude, settings.Longitude, units);
            var weather = await forecast.Get();
            if (weather == null || weather.currently == null || weather.daily == null) return null;
            var weatherData = new WeatherData
            {
                Location = settings.LocationName,
                Celsius = settings.Celsius,
                Icon = IconToMateocon(weather.currently.icon),
                CurrentTemperature = Temperature(weather.currently.temperature),
                FeelsLikeTemperature = Temperature(weather.currently.apparentTemperature),
                Summary = weather.currently.summary ?? "N/A",
                LongSummary = weather.daily.summary ?? "N/A",
                Wind = WindSummary(weather.currently.windSpeed, weather.currently.windBearing, settings.Celsius),
                Humidity = Probability(weather.currently.humidity),
                Time = weather.currently.time.ToDateTime().ToLocalTime().ToShortTimeString(),
                PoweredBy = "Powered by Forecast",
                Link = "http://forecast.io",
                Alert = (weather.alerts != null && weather.alerts.Count > 0) ? weather.alerts[0].title : string.Empty,
                AlertLink = (weather.alerts != null && weather.alerts.Count > 0) ? weather.alerts[0].uri : string.Empty,
                Forecasts = new ObservableCollection<WeatherForecast>(GetForecasts(weather.daily.data))
            };
            weatherData.Forecasts[0].Day = "Today";
            return weatherData;
        }

        private static string WindSummary(float speed, float bearing, bool celsius)
        {
            if (speed < 1) return "Calm";
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", Round(speed), celsius ? "kph" : "mph", WindRose(bearing));
        }

        private static string WindRose(float bearing)
        {
            if (bearing < 30) return "N";
            if (bearing < 60) return "NE";
            if (bearing < 110) return "E";
            if (bearing < 150) return "SE";
            if (bearing < 210) return "S";
            if (bearing < 240) return "SW";
            if (bearing < 300) return "W";
            if (bearing < 330) return "NW";
            return "N";
        }

        private static IEnumerable<WeatherForecast> GetForecasts(IEnumerable<DailyForecast> forecasts)
        {
            return forecasts.Select(f => new WeatherForecast
            {
                Day = f.time.ToDateTime().ToString("dddd"),
                Low = Temperature(f.temperatureMin),
                High = Temperature(f.temperatureMax),
                Icon = IconToMateocon(f.icon),
                Summary = f.summary,
                PrecipType = f.precipType,
                PrecipProbability = Probability(f.precipProbability)
            });
        }

        private static readonly Dictionary<string, string> MateoconsDictionary = new Dictionary<string, string>
        {
            {"clear-day", "B"},
            {"clear-night", "C"},
            {"rain", "R"},
            {"snow", "U"},
            {"sleet", "X"},
            {"wind", "F"},
            {"fog", "M"},
            {"cloudy", "N"},
            {"partly-cloudy-day", "H"},
            {"partly-cloudy-night", "I"},
            {"hail", "X"},
            {"thunderstorm", "O"},
            {"tornado", "F"}
        };

        private static string IconToMateocon(string icon)
        {
            string mateocon;
            return MateoconsDictionary.TryGetValue(icon ?? "", out mateocon) ? mateocon : ")";
        }

        private static string Temperature(float temperature)
        {
            return Round(temperature).ToString(CultureInfo.InvariantCulture);
        }

        private static float Round(float value)
        {
            return (float) Math.Round(value);
        }

        private static string Probability(float probability)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}%", Round(probability*100));
        }
    }
}