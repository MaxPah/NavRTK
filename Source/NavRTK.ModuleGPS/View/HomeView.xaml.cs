using Microsoft.Practices.Prism.Regions;
using NavRTK.ModuleGPS.ViewModel;
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
        }

        /// <summary>
        /// Initilize the data context of HomeView
        /// </summary>
        [Import]
        HomeViewModel viewmodel
        {
            get { return this.DataContext as HomeViewModel; }
            set { this.DataContext = value; }
        }
    }
}
