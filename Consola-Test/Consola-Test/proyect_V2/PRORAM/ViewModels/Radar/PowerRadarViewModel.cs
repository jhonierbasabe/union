using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase PowerRadarViewModel, controla la interación de la vista PowerRadarView
    /// </summary>
    public class PowerRadarViewModel: BindableBase, IInteractionRequestAware
    {

        #region private
        private int  _txPower;
        private IPowerRadarNotification _notification;
        private int _device;
        private bool setTx;
        #endregion

        /// <summary>
        /// Propiedad Device, index del dispositivo radar
        /// </summary>
        public int Device
        {
            get { return _device; }
            set {SetProperty(ref _device, value); }
        }
        /// <summary>
        /// Propiedad TXPower, valor de potentica de transmisición
        /// </summary>
        public int  TXPower
        {
            get { return _txPower; }
            set {SetProperty(ref _txPower, value); }
        }

        /// <summary>
        /// Constructor de la clase PowerRadarViewModel
        /// </summary>
        public PowerRadarViewModel()
        {
            setTx = true;            
            this.PropertyChanged += (s, e) =>  SetContent();
            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }
        /// <summary>
        /// Metodo SetContent, configura los valores iniciales una vez se ha cargado la vista
        /// </summary>
        private void SetContent()
        {
            if (setTx == true )
            {
                TXPower = _notification.TXPower;
                Device = _notification.Device;
                setTx = false;
            }
        }

        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public Action FinishInteraction { get; set; }

        /// <summary>
        /// Metodo CancelInteraction, cancel los cambios en la vista y la cierra
        /// </summary>
        private void CancelInteraction()
        {
            setTx = true;
            FinishInteraction?.Invoke();
        }
        /// <summary>
        /// Propiedad Submit, acepta los cambios y cierra la vista modal
        /// </summary>
        private void Submit()
        {
            setTx = true;
            _notification.Confirmed = true;
            _notification.TXPower = TXPower;
            FinishInteraction?.Invoke();
        }

        /// <summary>
        /// Propiedad Notification
        /// </summary>
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IPowerRadarNotification)value); }
        }
    }
}
