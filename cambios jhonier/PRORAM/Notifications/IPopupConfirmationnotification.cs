using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Notifications
{
    /// <summary>
    /// Interface IPopupConfirmationnotification para la implementacion de la ventana PopUp 
    /// </summary>
    public interface  IPopupConfirmationnotification: IConfirmation
    {
        string Message { get; set; }
        bool Responded { get; set; }

    }
}
