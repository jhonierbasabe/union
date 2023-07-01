using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase PopupConfirmationViewModel, controla la interaccion con la vista PopupConfirmationView
    /// </summary>
    public class PopupConfirmationViewModel : BindableBase, IInteractionRequestAware
    {
        #region private
        private string _title;
        private IPopupConfirmationnotification _notification;
        private string _content;
        #endregion

        public string Content
        {
            get { return _content; }
            set { SetProperty(ref _content, value); }
        }

        /// <summary>
        /// Constructor por defecto de la clase  PopupConfirmationViewModel
        /// </summary>
        public PopupConfirmationViewModel()
        {
            this.PropertyChanged +=(s,e) => this._notification.Message = SetContent();
            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }
        /// <summary>
        /// Metodo SetContent, actualiza los parametros de la vista con la notificacion
        /// </summary>
        /// <returns>retorna la notificación del mensaje para ser visualizado en la vista</returns>
        private string SetContent()
        {
            if(_notification.Message != null && _notification.Message != string.Empty)
            {
                Content = _notification.Message;
                return Content;
            }
            return string.Empty;
        }
        /// <summary>
        /// Metodo Submit, acepta los cambios y cierra la vista modal
        /// </summary>
        private void Submit()
        {
            _notification.Confirmed = true;
            _notification.Content = true;
            FinishInteraction?.Invoke();
        }
        /// <summary>
        /// Metodo CancelInteraction, cancela los cambios y cierra la vista modal
        /// </summary>
        private void CancelInteraction()
        {
            _notification.Content = false;
            FinishInteraction?.Invoke();
        }
        /// <summary>
        /// Propiedad Notification, que contiene la notificación generada al lanzar la vista modal
        /// </summary>
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IPopupConfirmationnotification)value); }
        }
        #region delegados
        public Action FinishInteraction { get; set; }
        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        #endregion
    }
}
