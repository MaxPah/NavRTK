using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition;
using NavRTK.ModuleGPS.Helper;
using System.Windows.Input;
using System.ComponentModel;

namespace NavRTK.ModuleGPS.ViewModel
{

    public class HomeViewModel : INotifyPropertyChanged
    {
        [ImportingConstructor]
        public HomeViewModel()
        {
        }

        #region COMMANDS
        /// <summary>
        /// 
        /// </summary>
        private RelayCommand switchToDataParsedView;
        public ICommand SwitchToDataParsedView
        {
            get
            {
                if (switchToDataParsedView == null)
                {
                    switchToDataParsedView = new RelayCommand(ExecuteSwitchToDataParsedView, CanSwitchToDataParsedView);
                }
                return switchToDataParsedView;
            }
        }
        
        private RelayCommand switchToSettingsView;
        public ICommand SwitchToSettingsView
        {
            get
            {
                if (switchToSettingsView == null)
                {
                    switchToSettingsView = new RelayCommand(ExecuteSwitchToSettingsView, CanSwitchToSettingsView);
                }
                return switchToSettingsView;
            }
        }
        #endregion

        #region COMMANDS IMPLEMENTATION
        private bool CanSwitchToDataParsedView()
        {
            return true;
        }
        private void ExecuteSwitchToDataParsedView()
        {
            System.Console.WriteLine("DataClicked");
            OnPropertyChanged("SwitchToDataParsedView");
        }
        private bool CanSwitchToSettingsView()
        {
            return true;
        }
        private void ExecuteSwitchToSettingsView()
        {
            System.Console.WriteLine("SettingsClicked");
            OnPropertyChanged("SwitchToSettingsView");
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion INotifyPropertyChanged Members
    }
}
