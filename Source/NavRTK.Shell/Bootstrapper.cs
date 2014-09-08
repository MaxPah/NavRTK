using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace NavRTK.Shell
{
    public class Bootstrapper : MefBootstrapper
    {
        private IRegionManager regionManager;
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }
        protected override void InitializeShell()
        {
            
            regionManager = this.Container.GetExportedValue<IRegionManager>();
            base.InitializeShell();
            Application.Current.MainWindow = (Shell)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(NavRTK.ModuleGPS.View.HomeView).Assembly));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();            
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog moduleCatalog = new ModuleCatalog();
            // this is the code responsible 
            // for adding ModuleGPS to the application

            moduleCatalog.AddModule(new ModuleInfo { ModuleName = "ModuleGPS" });

            return moduleCatalog;
        }
    }
}
