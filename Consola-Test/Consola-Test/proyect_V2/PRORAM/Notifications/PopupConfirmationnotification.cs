using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.Notifications
{
    /// <summary>
    /// Clase PopupConfirmationnotification implementa la interface IPopupConfirmationnotification
    /// </summary>
    public class PopupConfirmationnotification : Confirmation, IPopupConfirmationnotification
    {
        public  string Message { get; set; }
        
        public bool Responded { get; set; }
        public PopupConfirmationnotification()
        {
            this.Content = string.Empty;
            this.Responded = false;
        }
    }
}
