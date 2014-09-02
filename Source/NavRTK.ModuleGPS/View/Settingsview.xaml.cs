using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace NavRTK.ModuleGPS.View
{
    [Export]
    /// <summary>
    /// Interaction logic for Settingsview.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            this.DataContext = new NavRTK.ModuleGPS.ViewModel.SerialSettingsViewModel();
        }
    }
}
