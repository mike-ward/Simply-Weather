using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace SimplyWeather.Converters
{
    internal class TemperatureAlertToColorConverter : MarkupExtension, IMultiValueConverter
    {
        private static TemperatureAlertToColorConverter _converter;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _converter ?? (_converter = new TemperatureAlertToColorConverter());
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double temperature;
            double.TryParse((string) values[0], out temperature);
            var celsius = (bool)values[1];
            bool alert;
            if (celsius) alert = (temperature < -17.5 || temperature > 37.5);
            else alert = (temperature < 0 || temperature > 99.5);
            return alert ? Brushes.DarkRed : Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}