using System.Threading.Tasks;

namespace SimplyWeather.Models
{
    public interface IWeatherDataProvider
    {
        Task<WeatherData> GetWeather();
    }
}
