using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SimplyWeather.Converters
{
    internal class BoolToStringConverter : MarkupExtension, IValueConverter
    {
        // parameter is bar separated string (false value|true value)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string == false) return string.Empty;
            var parameters = ((string) parameter).Split('|');
            if (value is bool) return (bool) value ? parameters[1] : parameters[0];
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string == false || value is string == false) return false;
            var parameters = ((string) parameter).Split('|');
            return (string) value == parameters[1];
        }

        private static readonly BoolToStringConverter Converter = new BoolToStringConverter();
 
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Converter;
        }
    }
}