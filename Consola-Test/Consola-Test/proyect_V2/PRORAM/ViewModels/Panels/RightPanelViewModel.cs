using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using PRORAM.Models;
using PRORAM.Models.Shared;
using PRORAM.Models.TPC;
using PRORAM.ServicesTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase RightPanelViewModel, controla la interación con la vista RightPanelView
    /// </summary>
    public class RightPanelViewModel : BindableBase
    {
        #region private
        private IEventAggregator _ea;
        private IRegionManager _regionManager;
        private RadarDevicesModel radarDeviceModel;
        private Tracks _track;
        private Plots _plots;
        private bool _detailDevice;
        private bool _detailTrack;
        private bool _expanderPanelDevice;
        private string _uid;
        Timer timerClosePanel;

        private bool _detailPlotPanel;
        #endregion

        /// <summary>
        /// Propiedad DetailPlotPanel, propiedad bool que define  si esta mostrando el detalle de plots
        /// </summary>
        public bool DetailPlotPanel
        {
            get { return _detailPlotPanel; }
            set { SetProperty(ref _detailPlotPanel, value); }
        }
        /// <summary>
        /// Propiedad Plot, contiene información de un plot especifico
        /// </summary>
        public Plots Plot
        {
            get { return _plots; }
            set { SetProperty(ref _plots, value); }
        }
        /// <summary>
        /// Propiedad ExpanderPanelDevice, define si se expande el panel o no
        /// </summary>
        public bool ExpanderPanelDevice
        {
            get { return _expanderPanelDevice; }
            set { SetProperty(ref _expanderPanelDevice, value); }
        }
        /// <summary>
        /// Propiedad DetailTrack, define si el panel de detalle de track esta visible
        /// </summary>
        public bool DetailTrack
        {
            get { return _detailTrack; }
            set { SetProperty(ref _detailTrack, value); }
        }

        /// <summary>
        /// Propiedad DetailDevice, define si el detalle de un dispositivo radar esta visible
        /// </summary>
        public bool DetailDevice
        {
            get { return _detailDevice; }
            set { SetProperty(ref _detailDevice, value); }
        }

        /// <summary>
        /// Propiedad Track, contiene la informacion de un track especifico
        /// </summary>
        public Tracks Track
        {
            get { return _track; }
            set { SetProperty(ref _track, value); }
        }

        /// <summary>
        /// Propiedad RadarDeviceModel_, contiene la información de un dispositivo radar.        
        /// </summary>
        public RadarDevicesModel RadarDeviceModel_
        {
            get { return radarDeviceModel; }
            set { SetProperty(ref radarDeviceModel, value); }
        }

        /// <summary>
        /// Constructor de la clase RightPanelViewModel
        /// </summary>
        /// <param name="regionManager">parametro de redireccionamiento de la vista</param>
        /// <param name="ea">objecto referencia a los eventos generados por la aplicación</param>
        public RightPanelViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _ea = ea;
            _regionManager = regionManager;
            RadarDeviceModel_ = new RadarDevicesModel();
            Track = new Tracks();
            Plot = new Plots();
            _uid = String.Empty;
            DetailTrack = false;
            DetailDevice = false;
            DetailPlotPanel = false;

            _ea.GetEvent<EventPanel>().Subscribe(ShowDetail);
            _ea.GetEvent<SendEventDataSet>().Subscribe(UpdatePanel);          
        }

        /// <summary>
        /// Metodo UpdatePanel, actualiza y muestra la informacion del panel
        /// </summary>
        /// <param name="obj">objeto con la informacion a mostrar</param>
        private void UpdatePanel(EventsDataSet obj)
        {

            if (obj.evento == "GetDevicesList" && ExpanderPanelDevice == true)
            {
                var collectionDevices = DSconnection.DSConnection.GetDevicesList();

                if (collectionDevices.Count > 0)
                {
                    
                    var _index = collectionDevices.IndexOf(collectionDevices.Where(x => x.GuidRadar == RadarDeviceModel_.GuidRadar).First());
                    if (_index != -1)
                    {
                        DetailDevice = true;
                        DetailTrack = false;
                        Track = new Tracks();
                        _uid = string.Empty;
                        ExpanderPanelDevice = true;
                        RadarDeviceModel_ = collectionDevices[_index];
                    }
                }
            }

        }
        /// <summary>
        /// Metodo ShowDetail, muestra la informacion del panel
        /// </summary>
        /// <param name="obj"></param>
        private void ShowDetail(DetailPanel obj)
        {
            if (obj.Action == "Show")
            {
                if (obj.Target == "Tracks")
                {

                    RadarDeviceModel_ = new RadarDevicesModel();
                    Track = obj.Track;
                    ExpanderPanelDevice = true;
                    DetailTrack = true;
                    DetailDevice = false;
                    _uid = obj.Guid_;

                }
                if(obj.Action == "Plots")
                {

                    RadarDeviceModel_ = new RadarDevicesModel();
                    Track = new Tracks();
                    Plot = obj.Plots;
                    ExpanderPanelDevice = true;
                    
                    DetailTrack = false;
                    DetailDevice = false;
                    _uid = obj.Guid_;
                }
                if (obj.Target == "Device")
                {
                    DetailDevice = true;
                    DetailTrack = false;
                    Track = new Tracks();
                    _uid = string.Empty;
                    ExpanderPanelDevice = true;
                    RadarDeviceModel_ = obj.Device;
                }

            }
            if (obj.Action == "Clear" && obj.Target == "Track")
            {
                if (obj.Guid_ == _uid)
                {
                    _uid = String.Empty;
                    Track = new Tracks();
                    DetailDevice = false;
                    DetailTrack = false;
                    ExpanderPanelDevice = false;

                }
            }
            if (obj.Action == "Replace" && obj.Target == "Track" && ExpanderPanelDevice == true && _uid == obj.Guid_)
            {

                RadarDeviceModel_ = new RadarDevicesModel();
                Track = obj.Track;
                ExpanderPanelDevice = true;
                DetailTrack = true;
                DetailDevice = false;
                _uid = obj.Guid_;

            }


        }

    }
}

