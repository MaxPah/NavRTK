
using System.ComponentModel.Composition;
using System.Windows;

namespace NavRTK.Shell
{
    [Export]
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            this.InitializeComponent();
        }
    }
}
