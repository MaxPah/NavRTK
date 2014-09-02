using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace NavRTK.ModuleGPS.View
{
    [Export]
    /// <summary>
    /// Interaction logic for DataParsedView.xaml
    /// </summary>
    public partial class DataParsedView : UserControl
    {
        public DataParsedView()
        {
            InitializeComponent();
            this.DataContext = new NavRTK.ModuleGPS.ViewModel.DataParsedViewModel();
        }

        
    }
}
