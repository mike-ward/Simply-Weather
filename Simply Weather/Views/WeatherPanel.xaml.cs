using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using SimplyWeather.Models;

namespace SimplyWeather
{
    public partial class WeatherPanel
    {
        private readonly DispatcherTimer _timer = new DispatcherTimer();

        public WeatherPanel(IWeatherDataProvider weatherDataProvider)
        {
            InitializeComponent();
            DataContextChanged += (sender, args) => MainWindow.UpdateTaskbarIconCommand.Execute(DataContext, this);
            CommandBindings.Add(new CommandBinding(MainWindow.Switch7DayForecastCommand, Switch7DayForecastCommandHandler));
            Loaded += async (s, e) =>
            {
                _timer.Interval = TimeSpan.FromMinutes(10);
                _timer.Tick += async (o, eventArgs) => DataContext = await weatherDataProvider.GetWeather() ?? DataContext;
                _timer.Start();
                DataContext = await weatherDataProvider.GetWeather() ?? new WeatherData();
            };
            Unloaded += (s, e) => _timer.Stop();
        }

        private void HyperlinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void Switch7DayForecastCommandHandler(object sender, ExecutedRoutedEventArgs ea)
        {
            var weatherData = (WeatherData) DataContext;
            weatherData.Show7DayForecast = !weatherData.Show7DayForecast;
            MainWindow.Switch7DayForecastCommand.Execute(null, Application.Current.MainWindow);
        }
    }
}