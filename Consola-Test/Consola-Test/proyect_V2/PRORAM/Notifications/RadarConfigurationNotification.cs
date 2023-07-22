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
    /// Clase RadarConfigurationNotification implementa la interface IRadarConfigurationNotification
    /// </summary>
    public class RadarConfigurationNotification: Confirmation, IRadarConfigurationNotification
    {
        public RadarConfigurationModel RadarConfigurationModelI_ { get; set; }
        public Location Point1 { get; set; }
        public Location Point2 { get; set; }
        public double Teta { get; set; }
        public RadarConfigurationNotification()
        {
            RadarConfigurationModelI_ = new RadarConfigurationModel();            
        }        
    }
}
