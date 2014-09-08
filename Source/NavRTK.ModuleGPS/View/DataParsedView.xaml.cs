using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using NavRTK;
using NavRTK.ModuleGPS.ViewModel;

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
        }
    }
}
