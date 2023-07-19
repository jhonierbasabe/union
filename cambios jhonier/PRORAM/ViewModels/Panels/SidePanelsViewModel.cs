using Microsoft.Maps.MapControl.WPF;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using PRORAM.Models;
using PRORAM.Models.Shared;
using PRORAM.Models.TPC;
using PRORAM.Notifications;
using PRORAM.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase SidePanelsViewModel, controla la interación con la vista SidePanelsView
    /// </summary>
    public class SidePanelsViewModel : BindableBase 
    {
        #region private
        public ObservableCollection<RadarDevicesModel> _RadarDevicesModel;
        private RadarDevicesModel _sradarDevicesModel;
        private string _title;
        private string _mensaje;
        private string _titulo;
        private bool _expanderPanelDevice;
        private byte[] _buffer;
        private GeoLayerModel _geoLayerModel = new GeoLayerModel();
        private RadarConfigurationModel _RadarConfigurationModel;
        private TargetAreaModel _targetAreaModel = new TargetAreaModel();
        public Action RaiseRadarConfigurationInteraction { get; private set; }
        #endregion
        //RadarDevicesViewModel viewModel = new RadarDevicesViewModel();
        public  RadarDevicesViewModel radarDevicesViewModel;
        private IRadarDevicesNotification _notification;
       
        /// <summary>
        /// Propiedad ExpanderPanelDevice, define si el panal se encuentra visible
        /// </summary>
        public bool ExpanderPanelDevice
        {
            get { return _expanderPanelDevice; }
            set { SetProperty(ref _expanderPanelDevice, value); }
        }
        public RadarConfigurationModel RadarConfigurationModel_
        {
            get { return _RadarConfigurationModel; }
            set { _RadarConfigurationModel = value; }
        }
        /// <summary>
        /// Propiedad SRadarDevicesModel, contiene la informacion de dispositivo radar seleccionado
        /// </summary>
        /// 
        public TargetAreaModel TargetAreaMod
        {
            get { return _targetAreaModel; }
            set { SetProperty(ref _targetAreaModel, value); }
        }
        public GeoLayerModel GeoLayerModel_
        {
            get { return _geoLayerModel; }
            set { SetProperty(ref _geoLayerModel, value); }
        }
        public RadarDevicesModel SRadarDevicesModel
        {
            get { return _sradarDevicesModel; }
            set { SetProperty(ref _sradarDevicesModel, value); }
        }

        private IRegionManager _regionManager;
        private IEventAggregator _ea;
        private bool _transResult;

        /// <summary>
        /// Propiedad RadarDevicesModel_, collección de RadarDevicesModel_, contiene los dispositivos radar agregados
        /// </summary>
        public ObservableCollection<RadarDevicesModel> RadarDevicesModel_
        {
            get { return _RadarDevicesModel; }
            set { SetProperty(ref _RadarDevicesModel, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Contructor de la clase SidePanelsViewModel
        /// </summary>
        /// <param name="regionManager">parametro de redireccionamiento de la vista</param>
        /// <param name="ea">objecto referencia a los eventos generados por la aplicación</param>
        public SidePanelsViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            ExpanderPanelDevice = false;
            _regionManager = regionManager;
            _ea = ea;
            _ea.GetEvent<SendEventDataSet>().Subscribe(GetRadarDevices);
            
            DetailRadarCommand = new DelegateCommand(DetailRadar);
            CustomPopupRequest = new InteractionRequest<INotification>();
            _ea.GetEvent<MessageSentEvent>().Subscribe(LoadStageEvent);
            AgregarRadar = new DelegateCommand(_AgregarRadar);
            RadarConfigurationNotificationRequest = new InteractionRequest<IRadarConfigurationNotification>();

        }

    

        /// <summary>
        /// Metodo DetailRadar, publica evento para que se visualize la informacion de un dispositivo radar
        /// </summary>
        private void DetailRadar()
        {
            _ea.GetEvent<EventPanel>().Publish(new DetailPanel { Device = SRadarDevicesModel, Action = "Show", Target = "Device" });
        }

        private void LoadStageEvent(TargetAreaModel obj)
        {
            GeoLayerModel_.DefinedMap = true;
            TargetAreaMod = new TargetAreaModel { LatitudP1 = obj.LatitudP1, LatitudP2 = obj.LatitudP2, LongitudP1 = obj.LongitudP1, LongitudP2 = obj.LongitudP2, NombreArea = obj.NombreArea };
        }
        public void _AgregarRadar()
        {
            _titulo = "Mensaje de notificación ";
            _mensaje = "Necesita definir un área objetivo para poder agregar un dispositivo radar ";
            if (GeoLayerModel_.DefinedMap == true)
            {

                var p1 = new Location() { Latitude = TargetAreaMod.LatitudP1.Value, Longitude = TargetAreaMod.LongitudP1.Value };
                var p2 = new Location() { Latitude = TargetAreaMod.LatitudP2.Value, Longitude = TargetAreaMod.LongitudP2.Value };

                var count = RadarDevicesModel_.Count;
                var idRadar = count + 1;
                if (count <= 9)
                {
                    RadarConfigurationNotificationRequest.Raise(new RadarConfigurationNotification { Title = "Registrar radar", Content = idRadar, Point1 = p1, Point2 = p2 }, r =>
                    {
                        if (r.Confirmed && r.RadarConfigurationModelI_ != null)
                        {
                            RadarConfigurationModel_ = r.RadarConfigurationModelI_;
                            PushDevice();
                        }
                    });
                }
            }
            else
            {
                RaiseCustomPopup();
            }
            _titulo = string.Empty;
            _mensaje = string.Empty;
        }


        private RadarDevicesView _radarDevicesView;

        private void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = _titulo, Content = new { Text = _mensaje, Show = false, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
        }
        /// <summary>
        /// Propiedad RadarConfigurationNotificationRequest
        /// </summary>
        public InteractionRequest<IRadarConfigurationNotification> RadarConfigurationNotificationRequest { get; set; }
        /// <summary>
        /// Delegado RadarconfigurationNotificationCommand
        /// </summary>
        //public DelegateCommand RadarconfigurationNotificationCommand { get; private set; }
        
 
        public InteractionRequest<INotification> CustomPopupRequest { get; set; }
        public DelegateCommand DetailRadarCommand { get; set; }
        public DelegateCommand AgregarRadar { get; set; }


        public INotification Notification

        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IRadarDevicesNotification)value); }
        }
        /// <summary>
        /// Metodo GetRadarDevices, obtiene la lista de dispositivos radar del data set
        /// </summary>
        /// <param name="ds"></param>
        private void GetRadarDevices(EventsDataSet ds)
        {
            if (ds.evento == "GetDevicesList")
            {
                var count = DSconnection.DSConnection.GetDevicesList();
                RadarDevicesModel_ = count;
                if (RadarDevicesModel_.Count > 0)
                {
                    ExpanderPanelDevice = true;
                }
                else
                {
                    ExpanderPanelDevice = false;
                }
            }
        }

        private void PushDevice()
        {

            var radardevice = new RadarDevicesModel()
            {
                Port = 7023,
                Id = RadarConfigurationModel_.Id,
                Latitud = RadarConfigurationModel_.Latitud.Value,
                Longitud = RadarConfigurationModel_.Longitud.Value,
                Elevation = RadarConfigurationModel_.Elevation.Value,
                Azimuth = RadarConfigurationModel_.Azimuth.Value,
                IpAddress = RadarConfigurationModel_.IpAddress,
                Altitude = RadarConfigurationModel_.Altitude.Value,
                TXPower = RadarConfigurationModel_.TXPower.Value,
                InstallationAngle = RadarConfigurationModel_.InstallationAngle.Value,
                SChannelFrec = RadarConfigurationModel_.SchannelFrec,
                IdModelo = RadarConfigurationModel_.Modelo,
                StateConnection = false,
                Radiation = false,
                RadarName = RadarConfigurationModel_.RadarName,
                GuidRadar = RadarConfigurationModel_.Guid,
                MClutter = false,
                NorthHeiding = RadarConfigurationModel_.NorthHeiding.Value

            };
            DSconnection.DSConnection.AddRadarDeviceRow(radardevice);
            _ea.GetEvent<SendEventDataSet>().Publish(new EventsDataSet { evento = "GetDevicesList" });

            RadarDevicesModel_.Add(radardevice);
            _ea.GetEvent<MsmSentEvent>().Publish(new RadarActions()
            {
                GuidRadar = radardevice.GuidRadar,
                RadarDevice = radardevice,
                Action = "AddRadar",
                Logs = "Se agregó un nuevo dispositivo radar bajo el nombre de " + radardevice.RadarName
            });
        }

      


    }
}
