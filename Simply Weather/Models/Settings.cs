using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

// ReSharper disable once ExplicitCallerInfoArgument
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace SimplyWeather.Models
{
    internal class Settings : INotifyPropertyChanged, IDataErrorInfo
    {
        public string LocationName
        {
            get { return Properties.Settings.Default.LocationName; }
            set
            {
                if (Properties.Settings.Default.LocationName == value) return;
                Properties.Settings.Default.LocationName = value;
                OnPropertyChanged();
            }
        }

        public float Latitude
        {
            get { return Properties.Settings.Default.Latitude; }
            set
            {
                if (Properties.Settings.Default.Latitude == value) return;
                Properties.Settings.Default.Latitude = value;
                OnPropertyChanged();
            }
        }

        public float Longitude
        {
            get { return Properties.Settings.Default.Longitude; }
            set
            {
                if (Properties.Settings.Default.Longitude == value) return;
                Properties.Settings.Default.Longitude = value;
                OnPropertyChanged();
            }
        }

        public string ApiKey
        {
            get { return Properties.Settings.Default.ApiKey; }
            set
            {
                if (Properties.Settings.Default.ApiKey == value) return;
                Properties.Settings.Default.ApiKey = value;
                OnPropertyChanged();
            }
        }

        public bool Celsius
        {
            get { return Properties.Settings.Default.Celsius; }
            set
            {
                if (Properties.Settings.Default.Celsius == value) return;
                Properties.Settings.Default.Celsius = value;
                OnPropertyChanged();
            }
        }

        public bool Show7DayForecast
        {
            get { return Properties.Settings.Default.Show7DayForecast; }
            set
            {
                if (Properties.Settings.Default.Show7DayForecast == value) return;
                Properties.Settings.Default.Show7DayForecast = value;
                OnPropertyChanged();
            }
        }

        public static string Version
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Save()
        {
            Properties.Settings.Default.Save();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get
            {
                var result = string.Empty;
                switch (columnName)
                {
                    case "LocationName":
                        result = ValidateRequired(LocationName);
                        break;
                    case "Latitude":
                        result = ValidateLatitude();
                        break;
                    case "Longitude":
                        result = ValidateLongitude();
                        break;
                    case "ApiKey":
                        result = ValidateRequired(ApiKey);
                        break;
                }
                OnPropertyChanged("IsValid");
                return result;
            }
        }

        private string ValidateLatitude()
        {
            return ValidateRange(Latitude, -90, 90);
        }

        private string ValidateLongitude()
        {
            return ValidateRange(Longitude, -180, 180);
        }

        public bool IsValid
        {
            get
            {
                return ValidateRequired(LocationName) == string.Empty
                       && ValidateLatitude() == string.Empty
                       && ValidateLongitude() == string.Empty
                       && ValidateRequired(ApiKey) == string.Empty;
            }
        }

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        private static string ValidateRequired(string item)
        {
            return string.IsNullOrWhiteSpace(item) ? "Required" : string.Empty;
        }

        private static string ValidateRange(float value, float min, float max)
        {
            var result = (value >= min && value <= max)
                ? string.Empty
                : string.Format("{0} <= value <= {1}", min, max);
            return result;
        }
    }
}