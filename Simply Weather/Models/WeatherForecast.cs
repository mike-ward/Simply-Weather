namespace SimplyWeather.Models
{
    public class WeatherForecast : NotifyPropertyChanged
    {
        private string _day;
        private string _low;
        private string _high;
        private string _icon;
        private string _summary;
        private string _precipType;
        private string _precipProbability;

        public string Day
        {
            get { return _day; }
            set { SetProperty(ref _day, value); }
        }

        public string Low
        {
            get { return _low; }
            set { SetProperty(ref _low, value); }
        }

        public string High
        {
            get { return _high; }
            set { SetProperty(ref _high, value); }
        }

        public string Icon
        {
            get { return _icon; }
            set { SetProperty(ref _icon, value); }
        }

        public string Summary
        {
            get { return _summary; }
            set { SetProperty(ref _summary, value); }
        }

        public string PrecipType
        {
            get { return _precipType; }
            set { SetProperty(ref _precipType, value); }
        }

        public string PrecipProbability
        {
            get { return _precipProbability; }
            set { SetProperty(ref _precipProbability, value); }
        }
    }
}