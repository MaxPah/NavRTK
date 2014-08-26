using System.Composition;
using System.Windows;

namespace NavRTK.Shell
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>

    [Export(typeof(Shell))]
    public partial class Shell : Window
    {
        public Shell()
        {
            this.InitializeComponent();
        }
    }
}
