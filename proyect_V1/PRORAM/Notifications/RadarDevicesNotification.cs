using Microsoft.Maps.MapControl.WPF;
using Prism.Interactivity.InteractionRequest;
using PRORAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Notifications
{
    /// <summary>
    /// Clase RadarDevicesNotification implementa la interface IRadarDevicesNotification
    /// </summary>
    public class RadarDevicesNotification: Confirmation, IRadarDevicesNotification
    {
        public RadarDevicesModel RadarDevicesModel_ = new RadarDevicesModel();

        public RadarDevicesNotification()
        {
            RadarDevicesModel_ = null;
            this.Point1 = new Location();
            this.Point2 = new Location();
        }

        public Location Point1 { get; set; }
        public Location Point2 { get; set; }
        RadarDevicesModel IRadarDevicesNotification.RadarDevicesModel { get; set; }
      
    }
}
