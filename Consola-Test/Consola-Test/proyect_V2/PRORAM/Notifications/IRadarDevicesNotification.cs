using Microsoft.Maps.MapControl.WPF;
using Prism.Interactivity.InteractionRequest;
using PRORAM.Models;
using System.Threading.Tasks;

namespace PRORAM.Notifications
{
    /// <summary>
    /// Interface IRadarDevicesNotification para la implementación de la ventana Dispositivos radar
    /// </summary>
    public interface IRadarDevicesNotification: IConfirmation
    {
        RadarDevicesModel RadarDevicesModel { get; set; }

        Location Point1 { get; set; }

        Location Point2 { get; set; }

    }
}
