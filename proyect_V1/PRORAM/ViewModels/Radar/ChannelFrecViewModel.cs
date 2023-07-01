using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.Models;
using PRORAM.Notifications;
using PRORAM.ResourcesFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase ChannelFrecViewModel, controla la interación con la vista ChannelFrecView
    /// </summary>
    public class ChannelFrecViewModel : BindableBase, IInteractionRequestAware
    {
        #region private
        private ObservableCollection<Channels> _channel;
        private Channels _schannel;
        private IChannelFrecNotification _notification;
        private double _frecuency;
        private int _device;
        #endregion

        /// <summary>
        /// Propiedad Device, index del dispositivo radar
        /// </summary>
        public int Device
        {
            get { return _device; }
            set { SetProperty(ref _device, value); }
        }

        /// <summary>
        /// Propiedad Frecuency, frecuencia central del dispositivo radar
        /// </summary>
        public double Frecuency
        {
            get { return _frecuency; }
            set { SetProperty(ref _frecuency, value); }
        }

        /// <summary>
        /// Propiedad SChannel, canal de frecuencia seleccionado
        /// </summary>
        public Channels SChannel
        {
            get { return _schannel; }
            set { SetProperty(ref _schannel, value); }
        }
        /// <summary>
        /// Propiedad Channels, colección de canales de frecuencia
        /// </summary>
        public ObservableCollection<Channels> Channels
        {
            get { return _channel; }
            set { SetProperty(ref _channel, value); }
        }
        /// <summary>
        /// Constructor de la clase  ChannelFrecViewModel
        /// </summary>
        public ChannelFrecViewModel()
        {
            LoadedCommand = new DelegateCommand(Loaded);
            SubmitCommand = new DelegateCommand(Submit);
            CancelCommand = new DelegateCommand(CancelInteraction);
        }


        /// <summary>
        /// Metodo Loaded, es lanzado cuando la vista es renderisado y actuliza la informacion a mostrar
        /// </summary>
        private void Loaded()
        {
            var indexC = _notification.IndexChannel-1;
            var modelos = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar;
            Device = _notification.Device;

            Channels = new ObservableCollection<Channels>();
            if (_notification.ModeloS == 1)
            {
                foreach (var i in modelos.Modelo1.ChannelFrec)
                {
                    Channels.Add(new Channels
                    {
                        Id = i.Channel,
                        Frecuency = i.Frec,
                        DisplayName = i.Channel + " - " + i.Frec+ " GHz"
                    });
                }
                Frecuency = modelos.Modelo1.ChannelFrec[indexC].Frec;
            }
            if (_notification.ModeloS == 2)
            {

                foreach (var i in modelos.Modelo2.ChannelFrec)
                {
                    Channels.Add(new Channels
                    {
                        Id = i.Channel,
                        Frecuency = i.Frec,
                        DisplayName = i.Channel + " - " + i.Frec+ " GHz"
                    });
                }
                Frecuency = modelos.Modelo2.ChannelFrec[indexC].Frec;
                SChannel = Channels.Where(x => x.Id == _notification.IndexChannel).FirstOrDefault();
            }


        }
        /// <summary>
        /// Propiedad Notificacition
        /// </summary>
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IChannelFrecNotification)value); }
        }
        /// <summary>
        /// Metodo CancelInteraction, finaliza la visualización de la vista modal
        /// </summary>
        private void CancelInteraction()
        {
            FinishInteraction?.Invoke();
        }
        /// <summary>
        /// Metodo Submit, acepta los cambios realizados en la vista
        /// </summary>
        private void Submit()
        {
            _notification.Confirmed = true;
            _notification.Channelfrec = SChannel;
            FinishInteraction?.Invoke();
        }

        public DelegateCommand SubmitCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public Action FinishInteraction { get; set; }

        public DelegateCommand LoadedCommand { get; set; }

    }
}
