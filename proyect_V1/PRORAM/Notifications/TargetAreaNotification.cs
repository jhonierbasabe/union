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
    /// Clase TargetAreaNotification implementa la interface ITargetAreaNotification
    /// </summary>
    public class TargetAreaNotification : Confirmation, ITargetAreaNotification
    {
        public TargetAreaModel TargetAreaModel_ = new TargetAreaModel();

        public TargetAreaNotification()
        {
            TargetAreaModel_ = null;
        }
        TargetAreaModel ITargetAreaNotification.TargetAreaModel { get; set; }
    }
}
