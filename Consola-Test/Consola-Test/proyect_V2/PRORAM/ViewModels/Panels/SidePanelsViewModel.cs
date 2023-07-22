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
        private string _title;
        private bool _expanderPanelDevice;
        private byte[] _buffer;
        #endregion

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
            DetailRadarCommand = new DelegateCommand(DetailRadar);

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

    }
}
