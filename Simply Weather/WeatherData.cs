using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SimplyWeather.Models
{
    internal class WeatherData : INotifyPropertyChanged
    {
        public string Location { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}