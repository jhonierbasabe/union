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
    /// Interface IRadarConfigurationNotification para el manejo de la ventana de cambio de configuración del radar
    /// </summary>
    public interface IRadarConfigurationNotification : IConfirmation
    {
        RadarConfigurationModel RadarConfigurationModelI_ { get; set; }

        Location Point1 { get; set; }
        Location Point2 { get; set; }
        double Teta { get; set; }
    }
}
