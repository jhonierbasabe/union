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
    /// Interface ITargetAreaNotification para la implementación de la ventana Area de vigilancia
    /// </summary>
    public interface ITargetAreaNotification : IConfirmation
    {
        TargetAreaModel TargetAreaModel { get; set; }
    }
}
