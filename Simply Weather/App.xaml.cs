using System.Windows;
using SimplyWeather.Properties;
using TinyIoC;

namespace SimplyWeather
{
    public partial class App
    {
        private void OnStart(object sender, StartupEventArgs e)
        {
            if (Settings.Default.UpgradeSettings)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeSettings = false;
                Settings.Default.Save();
            }

            TinyIoCContainer.Current.AutoRegister();
            MainWindow = TinyIoCContainer.Current.Resolve<MainWindow>();
            MainWindow.Show();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }

        private void OnSessionEnding(object sender, SessionEndingCancelEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}