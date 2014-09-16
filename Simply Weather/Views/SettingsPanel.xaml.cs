using System.Diagnostics;
using System.Windows.Navigation;
using SimplyWeather.Models;

namespace SimplyWeather
{
    public partial class SettingsPanel
    {
        public SettingsPanel()
        {
            InitializeComponent();
            Unloaded += (s, e) => ((Settings) DataContext).Save();
        }

        public bool AreValid()
        {
            return ((Settings) DataContext).IsValid;
        }

        private void HyperlinkOnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}