using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimplyWeather.Converters
{
    [ValueConversion(typeof (bool), typeof (Visibility))]
    public class InvertableBooleanToVisibilityConverter : IValueConverter
    {
        private enum Parameters
        {
            Normal,
            Inverted
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var boolValue = (bool) value;
            Parameters direction;
            Enum.TryParse(parameter as string, out direction);

            if (direction == Parameters.Inverted)
                return !boolValue ? Visibility.Visible : Visibility.Collapsed;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}