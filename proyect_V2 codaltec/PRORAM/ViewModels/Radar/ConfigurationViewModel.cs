using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.DataValidation;
using PRORAM.Models;
using PRORAM.Notifications;
using PRORAM.ResourcesFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase ConfigurationViewModel, controla la interación con la vista ConfigurationView
    /// </summary>
    public class ConfigurationViewModel : ValidatableBindableBase, IInteractionRequestAware
    {


        #region private
        private IRadarConfigurationNotification _notification;
        private RadarConfigurationModel _radarConfigurationMod;
        private IList<SelectedItem> _Errors;
        private string _tituloError;
        private string _mensaje;
        private ObservableCollection<Channels> _channel;
        #endregion

        /// <summary>
        /// Propiedad Channels, colección de canales de frecuencia
        /// </summary>
        public ObservableCollection<Channels> Channels
        {
            get { return _channel; }
            set { SetProperty(ref _channel, value); }
        }
        /// <summary>
        /// Propiedad RadarConfigurationModel, modelo de radar configuración para la vista
        /// </summary>
        public RadarConfigurationModel RadarConfigurationMod
        {
            get { return _radarConfigurationMod; }
            set { SetProperty(ref _radarConfigurationMod, value); }
        }



        /// <summary>
        /// Metodo que busca por errores en las propiedades del modelo
        /// </summary>
        /// <returns> lista con los errores encontrados</returns>
        private IList<SelectedItem> FlattenErrors()
        {
            IList<SelectedItem> errors = new List<SelectedItem>();
            Dictionary<string, List<string>> allErrors = RadarConfigurationMod.GetAllErrors();
            foreach (string propertyName in allErrors.Keys)
            {
                foreach (var errorString in allErrors[propertyName])
                {
                    //,propertyName + ": " + errorString
                    errors.Add(
                        new SelectedItem
                        {
                            Property = propertyName,
                            MessageError = propertyName + ": " + errorString
                        });
                }
            }

            return errors;
        }


        /// <summary>
        /// Propiedad Errors, contiene los errores de los campos de la vista
        /// </summary>
        public IList<SelectedItem> Errors
        {
            get { return _Errors; }
            set { SetProperty(ref _Errors, value); }
        }
        /// <summary>
        /// Contructor de la clase ConfiguractionViewModel
        /// </summary>
        public ConfigurationViewModel()
        {
            Channels = new ObservableCollection<Channels>();
            RadarConfigurationMod = new RadarConfigurationModel();
            RadarConfigurationMod.ErrorsChanged += (s, e) => Errors = FlattenErrors();
            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);
            OnLoadScreenCommand = new DelegateCommand(OnLoadScreen);
            CancelInteractionCommand = new DelegateCommand(CancelInteraction);

            SubmitCommand = new DelegateCommand(Submit);
        }

        /// <summary>
        /// Propiedad OnLoadScreen, configurala vista cada vez que le carga
        /// </summary>
        private void OnLoadScreen()
        {
            Channels = new ObservableCollection<Channels>();
            RadarConfigurationMod = _notification.RadarConfigurationModelI_;
            RadarConfigurationMod.Elevation = 0;
            RadarConfigurationMod.TXPower = _notification.RadarConfigurationModelI_.TXPower.Value;
            RadarConfigurationMod.SchannelFrec = _notification.RadarConfigurationModelI_.SchannelFrec;
            RadarConfigurationMod.ChannelFrec = new ObservableCollection<Channels>();
            var modelos = TextResources.GetJsonContent().PRORAM_RESOURCE_FILE.ModelosRardar;
            foreach (var i in modelos.Modelo2.ChannelFrec)
            {
                RadarConfigurationMod.ChannelFrec.Add(new Channels
                {
                    Id = i.Channel,
                    Frecuency = i.Frec,
                    DisplayName = i.Channel + " - " + i.Frec + " GHz"
                });
            }
            var chan = RadarConfigurationMod.ChannelFrec.Where(x => x.Frecuency == RadarConfigurationMod.SchannelFrec.Frecuency).FirstOrDefault();


            RadarConfigurationMod.SchannelFrec = chan;
        }
        /// <summary>
        /// Metodo CancelInteraction, cancel los cambios en la vista y la cierra
        /// </summary>
        private void CancelInteraction()
        {

            _notification.RadarConfigurationModelI_ = null;
            _notification.Confirmed = false;

            FinishInteraction?.Invoke();
        }

        /// <summary>
        /// Propiedad FinishInteraction, controla las notificaciones de incio y cierre de la vista
        /// </summary>
        public Action FinishInteraction { get; set; }
        /// <summary>
        /// Propiedad Notification
        /// </summary>
        public INotification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IRadarConfigurationNotification)value); }
        }

        public InteractionRequest<INotification> CustomPopupRequest { get; set; }

        public DelegateCommand CustomPopupCommand { get; set; }
        public DelegateCommand CancelInteractionCommand { get; set; }
        public DelegateCommand OnLoadScreenCommand { get; set; }
        public DelegateCommand SubmitCommand { get; set; }
        public string Tittle { get; private set; }

        /// <summary>
        /// Propiedad Submit, acepta los cambios y cierra la vista modal
        /// </summary>
        private void Submit()
        {
            RadarConfigurationMod.ValidateProperties();
            Errors = FlattenErrors();
            if (!RadarConfigurationMod.HasErrors)
            {
                _tituloError = "Notificación";
                _mensaje = "Se ha configurado los parámetros del radar de forma exitosa";
                RaiseCustomPopup();
                _tituloError = "";
                _mensaje = "";

                RadarConfigurationMod.TXPower = RadarConfigurationMod.TXPower.Value;
                _notification.RadarConfigurationModelI_ = RadarConfigurationMod;
                _notification.Confirmed = true;
                RadarConfigurationMod = new RadarConfigurationModel();
                FinishInteraction?.Invoke();
            }
            else
            {
                _tituloError = "Alerta";
                _mensaje = "Debe diligenciar todos los campos";
                RaiseCustomPopup();
                _tituloError = "";
                _mensaje = "";
            }
        }

        /// <summary>
        /// Metodo RaiseCustomPopup, invoca la ventana de notificación
        /// </summary>
        private void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = _tituloError, Content = new { Text = _mensaje, Show = false } }, r => Tittle = "PRORAM Consola de monitoreo");
        }
    }
}
