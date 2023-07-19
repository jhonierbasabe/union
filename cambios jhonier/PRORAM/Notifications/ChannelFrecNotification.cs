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
    /// Clase ChannelFrecNotification, implementa la interface IChannelFrecNotification
    /// </summary>
    public class ChannelFrecNotification : Confirmation, IChannelFrecNotification
    {
        public int ModeloS { get; set; }
        public int Device { get; set; }
        public Channels Channelfrec { get; set; }

        public int IndexChannel {get;set;}

        public ChannelFrecNotification()
        {
            ModeloS = 0;
            IndexChannel = 0;
            Device = 0;
            Channelfrec = new Channels();
        }
    }
}
