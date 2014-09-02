using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace NavRTK.ModuleGPS
{   
    [ModuleExport(typeof(ModuleGPS))]
    class ModuleGPS : IModule
    {
        [Import]
        public IRegionManager regionManager { private get; set; }

        public void Initialize()
        {
            System.Console.WriteLine("ModuleGPSChargé");
            regionManager.RegisterViewWithRegion("RootRegion", typeof(NavRTK.ModuleGPS.View.HomeView));
            regionManager.RegisterViewWithRegion("MainRegion", typeof(NavRTK.ModuleGPS.View.SettingsView));
            //regionManager.RegisterViewWithRegion("MainRegion", typeof(NavRTK.ModuleGPS.View.DataParsedView));
        }

    }
}
