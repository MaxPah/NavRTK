using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

using NavRTK.ModuleGPS.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace NavRTK.ModuleGPS
{
    [ModuleExport(typeof(ModuleGPS))]
    class ModuleGPS : IModule
    {
        [Export]
        public IRegionManager regionManager { private get; set; }

        #region Constructor
        public void Initialize()
        {
            System.Console.WriteLine("ModuleGPSChargé");
            regionManager.RegisterViewWithRegion("RootRegion", typeof(NavRTK.ModuleGPS.View.HomeView));
            regionManager.RegisterViewWithRegion("MainRegion", typeof(NavRTK.ModuleGPS.View.DataParsedView));  
            regionManager.RegisterViewWithRegion("MainRegion", typeof(NavRTK.ModuleGPS.View.SettingsView));
        }
        #endregion
    }
}
