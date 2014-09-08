using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using NavRTK.ModuleGPS.ViewModel;

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
        }
    }
}
