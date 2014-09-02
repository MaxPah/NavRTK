using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NavRTK.Shell
{
    public class Bootstrapper : MefBootstrapper
    {        

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }
        protected override void InitializeShell()
        {
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
            // for adding Module1 to the application
            moduleCatalog.AddModule(new ModuleInfo
                {   InitializationMode = InitializationMode.WhenAvailable,
                    ModuleName = "ModuleGPS"    });

            return moduleCatalog;
        }
    }
}
