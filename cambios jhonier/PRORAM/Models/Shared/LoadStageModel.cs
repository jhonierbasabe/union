using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Models.Shared
{
    /// <summary>
    /// Clase LoadStageModel, modelo escenario establecido en la aplicación
    /// </summary>
    public class LoadStageModel : BindableBase
    {
        private SurveillanceAreaModel _surveillanceArea;
        private ObservableCollection<RadarDevicesModel> _devices;
        /// <summary>
        /// Propiedad SurveillanceArea, modelo del área de vigilancia definida
        /// </summary>
        public SurveillanceAreaModel SurveillanceArea
        {
            get { return _surveillanceArea; }
            set { SetProperty(ref _surveillanceArea, value); }
        }
        /// <summary>
        /// Propiedad Devices, coleccíon de disppositivos radares definidos en el escenario
        /// </summary>
        public ObservableCollection<RadarDevicesModel> Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices, value); }
        }

    }
}
