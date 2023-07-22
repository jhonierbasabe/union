using Prism.Interactivity.InteractionRequest;
using System;

namespace PRORAM.Notifications
{
    /// <summary>
    /// Interface IPowerRadarNotification para la implementacion de la ventana de cambio de potencia del radar
    /// </summary>
    public interface IPowerRadarNotification : IConfirmation
    {
        int TXPower { get; set; }
        int Device { get; set; }
        Guid GuidRadar { get; set; }
    }
}
