using System.Collections.ObjectModel;

namespace SimplyWeather.Models
{
    public class WeatherData : NotifyPropertyChanged
    {
        private string _location = string.Empty;
        private string _icon = string.Empty;
        private string _currentTemperature = string.Empty;
        private string _feelsLikeTemperature = string.Empty;
        private string _summary = string.Empty;
        private string _longSummary = string.Empty;
        private string _wind = string.Empty;
        private string _humidity = string.Empty;
        private string _poweredBy = string.Empty;
        private string _link = string.Empty;
        private string _time = string.Empty;
        private string _alert = string.Empty;
        private string _alertLink = string.Empty;
        private bool _celsius;

        public ObservableCollection<WeatherForecast> Forecasts { get; set; }

        public string Location
        {
            get { return _location; }
            set { SetProperty(ref _location, value); }
        }

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        public string CurrentTemperature
        {
            get { return _currentTemperature; }
            set { SetProperty(ref _currentTemperature, value); }
        }

        public string FeelsLikeTemperature
        {
            get { return _feelsLikeTemperature; }
            set { SetProperty(ref _feelsLikeTemperature, value); }
        }

        public string Summary
        {
            get { return _summary; }
            set { SetProperty(ref _summary, value); }
        }

        public string LongSummary
        {
            get { return _longSummary; }
            set { SetProperty(ref _longSummary, value); }
        }

        public string Wind
        {
            get { return _wind; }
            set { SetProperty(ref _wind, value); }
        }

        public string Humidity
        {
            get { return _humidity; }
            set { SetProperty(ref _humidity, value); }
        }

        public string Link
        {
            get { return _link; }
            set { SetProperty(ref _link, value); }
        }

        public string PoweredBy
        {
            get { return _poweredBy; }
            set { SetProperty(ref _poweredBy, value); }
        }

        public string Time
        {
            get { return _time; }
            set { SetProperty(ref _time, value); }
        }

        public string Alert
        {
            get { return _alert; }
            set
            {
                SetProperty(ref _alert, value);
                OnPropertyChanged("HasAlert");
            }
        }

        public string AlertLink
        {
            get { return _alertLink; }
            set { SetProperty(ref _alertLink, value); }
        }

        public bool HasAlert
        {
            get { return string.IsNullOrEmpty(_alert) == false; }
        }

        public bool Celsius
        {
            get { return _celsius; }
            set { SetPropertyValue(ref _celsius, value); }
        }

        public bool Show7DayForecast
        {
            get { return Properties.Settings.Default.Show7DayForecast; }
            set
            {
                if (Properties.Settings.Default.Show7DayForecast == value) return;
                Properties.Settings.Default.Show7DayForecast = value;
                OnPropertyChanged("Show7DayForecast");
            }
        }
    }
}