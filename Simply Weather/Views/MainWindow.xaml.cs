using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SimplyWeather.Models;
using Settings = SimplyWeather.Properties.Settings;

// ReSharper disable once CheckNamespace

namespace SimplyWeather
{
    public partial class MainWindow
    {
        private readonly WeatherPanel _weatherPanel;
        private readonly SettingsPanel _settingsPanel;
        public static RoutedCommand SettingsPanelCommand = new RoutedUICommand();
        public static RoutedCommand WeatherPanelCommand = new RoutedUICommand();
        public static RoutedCommand Switch7DayForecastCommand = new RoutedCommand();
        public static RoutedCommand UpdateTaskbarIconCommand = new RoutedCommand();

        public MainWindow(WeatherPanel weatherPanel, SettingsPanel settingsPanel)
        {
            if (BuildInfo.HasExpired())
            {
                Close();
                return;
            }
            InitializeComponent();
            SetWindowHeight();
            ApplyCommandBindings();
            _weatherPanel = weatherPanel;
            _settingsPanel = settingsPanel;
            Content = _settingsPanel.AreValid() ? (object)_weatherPanel : _settingsPanel;
        }

        private void SetWindowHeight()
        {
            Height = Settings.Default.Show7DayForecast ? 695 : 465;
        }

        private void ApplyCommandBindings()
        {
            CommandBindings.Add(new CommandBinding(SettingsPanelCommand, SettingsPanelCommandHandler));
            CommandBindings.Add(new CommandBinding(WeatherPanelCommand, WeatherPanelCommandHandler));
            CommandBindings.Add(new CommandBinding(Switch7DayForecastCommand, Switch7DayForecastCommandHandler));
            CommandBindings.Add(new CommandBinding(UpdateTaskbarIconCommand, UpdateTaskbarIconCommandHandler));
        }

        private void MainWindowOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void SettingsPanelCommandHandler(object sender, ExecutedRoutedEventArgs ea)
        {
            if (Content.Equals(_settingsPanel) == false) Content = _settingsPanel;
        }

        private void WeatherPanelCommandHandler(object sender, ExecutedRoutedEventArgs ea)
        {
            if (Content.Equals(_weatherPanel) == false) Content = _weatherPanel;
        }

        private void Switch7DayForecastCommandHandler(object sender, ExecutedRoutedEventArgs ea)
        {
            SetWindowHeight();
        }

        public void UpdateTaskbarIconCommandHandler(object sender, ExecutedRoutedEventArgs ea)
        {
            var weatherData = (WeatherData)ea.Parameter;
            var temperature = weatherData.CurrentTemperature.Substring(Math.Max(0, weatherData.CurrentTemperature.Length - 2));
            var text = new FormattedText(temperature,
                CultureInfo.GetCultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface("Segoe UI Bold"),
                14, 
                weatherData.CurrentTemperature.StartsWith("-") ? Brushes.Red : Brushes.Black);

            var bitmap = CreateBitmap(drawingContext => drawingContext.DrawText(text, new Point(0, 0)));
            TaskbarItemInfo.Overlay = bitmap;
        }

        private BitmapSource CreateBitmap(Action<DrawingContext> render)
        {
            var source = PresentationSource.FromVisual(this);
            var dpiX = 96.0 * source.CompositionTarget.TransformToDevice.M11;
            var dpiY = 96.0 * source.CompositionTarget.TransformToDevice.M22;
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen()) render(drawingContext);
            var bitmap = new RenderTargetBitmap(16, 16, dpiX, dpiY, PixelFormats.Default);
            bitmap.Render(drawingVisual);
            return bitmap;
        }
    }
}