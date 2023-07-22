using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Notifications
{
    /// <summary>
    /// Clase PowerRadarNotification implementa la interface IPowerRadarNotification
    /// </summary>
    public class PowerRadarNotification: Confirmation, IPowerRadarNotification
    {
        public int TXPower { get; set; }
        public int Device { get; set; }
        public Guid GuidRadar { get; set; }

        public PowerRadarNotification()
        {
            this.Device = 0;
            this.TXPower = 0;
            this.GuidRadar = new Guid();
        }
    }
}
