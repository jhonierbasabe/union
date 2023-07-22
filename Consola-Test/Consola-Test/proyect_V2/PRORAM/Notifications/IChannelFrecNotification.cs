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
    /// Interface IChannelFrecNotification para la implementación de la ventana de cambio de frecuencia
    /// </summary>
    public interface IChannelFrecNotification: IConfirmation
    {
        int ModeloS { get; set; }
        int Device { get; set; }
        int IndexChannel { get; set; }

        Channels Channelfrec { get; set; }
    }
}
