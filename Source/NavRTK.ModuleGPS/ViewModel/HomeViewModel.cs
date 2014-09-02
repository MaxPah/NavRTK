using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;
using System.ComponentModel.Composition;
using NavRTK.ModuleGPS.Helper;
using NavRTK.ModuleGPS.View;
using System.Windows.Input;
using System.ComponentModel;
using System;

namespace NavRTK.ModuleGPS.ViewModel
{

    public class HomeViewModel : INotifyPropertyChanged
    {
        //RegionManager regionManager;

        [ImportingConstructor]
        public HomeViewModel()
        {
            //regionManager.RequestNavigate("MainRegion","NavRTK.ModuleGPS.View.DataParsedView");
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
            //regionManager.RequestNavigate("MainRegion", "SettingsView");
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
