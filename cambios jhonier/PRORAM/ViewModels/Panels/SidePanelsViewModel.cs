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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase SidePanelsViewModel, controla la interación con la vista SidePanelsView
    /// </summary>
    public class SidePanelsViewModel : BindableBase
    {


        #region private
        private ObservableCollection<RadarDevicesModel> _radarDevicesModel;
        private RadarDevicesModel _sradarDevicesModel;
        private TargetAreaModel _targetAreaModel = new TargetAreaModel();
        private GeoLayerModel _geoLayerModel = new GeoLayerModel();
        private string _title;
        private bool _expanderPanelDevice;
        private byte[] _buffer;
        #endregion


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
        /// <summary>
        /// Propiedad ExpanderPanelDevice, define si el panal se encuentra visible
        /// </summary>
        public bool ExpanderPanelDevice
        {
            get { return _expanderPanelDevice; }
            set { SetProperty(ref _expanderPanelDevice, value); }
        }

        /// <summary>
        /// Propiedad SRadarDevicesModel, contiene la informacion de dispositivo radar seleccionado
        /// </summary>
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
            get { return _radarDevicesModel; }
            set { SetProperty(ref _radarDevicesModel, value); }
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
            RadarDevicesModel_ = new ObservableCollection<RadarDevicesModel>();
            _ea.GetEvent<MessageSentEvent>().Subscribe(LoadStageEvent);
            DetailRadarCommand = new DelegateCommand(DetailRadar);

        }


        private void LoadStageEvent(TargetAreaModel obj)
        {
            GeoLayerModel_.DefinedMap = true;
            TargetAreaMod = new TargetAreaModel { LatitudP1 = obj.LatitudP1, LatitudP2 = obj.LatitudP2, LongitudP1 = obj.LongitudP1, LongitudP2 = obj.LongitudP2, NombreArea = obj.NombreArea };
        }

        /// <summary>
        /// Metodo DetailRadar, publica evento para que se visualize la informacion de un dispositivo radar
        /// </summary>
        private void DetailRadar()
        {
            _ea.GetEvent<EventPanel>().Publish(new DetailPanel { Device = SRadarDevicesModel, Action = "Show", Target = "Device" });
        }



        /// <summary>
        /// Propiedad RadarConfigurationNotificationRequest
        /// </summary>
        public InteractionRequest<IRadarConfigurationNotification> RadarConfigurationNotificationRequest { get; set; }
        /// <summary>
        /// Delegado RadarconfigurationNotificationCommand
        /// </summary>
        public DelegateCommand RadarconfigurationNotificationCommand { get; private set; }

        public DelegateCommand DetailRadarCommand { get; set; }

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
                foreach (RadarDevicesModel radarDevice in RadarDevicesModel_)
                {
                    radarDevice.IdTextColor = GetColorById(Convert.ToInt32(radarDevice.Id));
                }
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

        public static SolidColorBrush GetColorById(int i)
        {
            switch (i)
            {
                case 1: return Brushes.DeepSkyBlue;
                case 2: return Brushes.DarkOrange;
                case 3: return Brushes.Gold;
                case 4: return Brushes.LightCoral;
                case 5: return Brushes.LimeGreen;
                case 7: return Brushes.Orchid;
                case 8: return Brushes.Plum;
                case 9: return Brushes.RoyalBlue;
                case 10: return Brushes.Tomato;

                default: return Brushes.Beige;
            }

        }

    }
}
