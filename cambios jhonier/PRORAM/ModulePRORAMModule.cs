using Prism.Ioc;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRORAM.Views;
using Prism.Regions;

namespace PRORAM
{
    public class ModulesPRORAMModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion("GeoLayer", typeof(GeoLayerView));
            regionManager.RegisterViewWithRegion("RightPanel", typeof(RightPanelView));
            regionManager.RegisterViewWithRegion("SidePanels", typeof(SidePanelsView));
            regionManager.RegisterViewWithRegion("TargetPanel", typeof(TargetPanelView));
            regionManager.RegisterViewWithRegion("LogsView", typeof(LogsView));

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainWindow>();
        }
    }
}
