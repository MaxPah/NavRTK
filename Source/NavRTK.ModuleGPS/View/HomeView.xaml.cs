using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace NavRTK.ModuleGPS.View
{
    [Export]
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            this.DataContext = new NavRTK.ModuleGPS.ViewModel.HomeViewModel();
        }
    }
}
