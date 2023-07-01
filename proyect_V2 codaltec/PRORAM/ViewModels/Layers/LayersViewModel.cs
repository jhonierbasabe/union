using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.Models;
using PRORAM.Models.Layers;
using PRORAM.Models.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase LayersViewModel, controla la interacción la vista Layers
    /// </summary>
    public class LayersViewModel : BindableBase, IInteractionRequestAware
    {
        #region private
        IEventAggregator _ea;
        private ObservableCollection<LayersModel> _layerModel;
        private LayersModel _slayerModel;
        private ObservableCollection<RadarDevicesModel> _devices;
        #endregion 

        /// <summary>
        /// Propiedad Notification, controla la interación con la vista emergente
        /// </summary>
        public INotification Notification { get; set; }
 
        /// <summary>
        /// Propiedad Devices, colección de dispositivos radar
        /// </summary>
        public ObservableCollection<RadarDevicesModel> Devices
        {
            get { return _devices; }
            set {SetProperty(ref _devices, value); }
        }

        /// <summary>
        /// Proiedad SLayerModel, modelo seleccionada de capa
        /// </summary>
        public LayersModel SLayerModel
        {
            get { return _slayerModel; }
            set { SetProperty(ref _slayerModel, value); }
        }
        /// <summary>
        /// Metodo LayerModel, coleccion de modelos de cpas
        /// </summary>
        public ObservableCollection<LayersModel> LayerModel
        {
            get { return _layerModel; }
            set { SetProperty(ref _layerModel, value); }
        }

        #region Delegados
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand UpdateLayersCommand { get; set; }
        public DelegateCommand LoadScreenCommand { get; set; }
        public Action FinishInteraction { get; set; }
        #endregion

        /// <summary>
        /// Constructor por defecto de la clase LayersViewModel
        /// </summary>
        /// <param name="ea">objeto que maneja los eventos realizados en la aplicación</param>
        public LayersViewModel(IEventAggregator ea)
        {
            LayerModel = DSconnection.DSConnection.GetLayers();
            Devices = DSconnection.DSConnection.GetDevicesList(); 
            _ea = ea;                                              
            CancelCommand = new DelegateCommand(Cancel);
            UpdateLayersCommand = new DelegateCommand(UpdateLayer);
            LoadScreenCommand = new DelegateCommand(LoadScreen);
            
        }
        /// <summary>
        /// Metodo LoadScreen, actualiza los dispositivos radar cada vez que se carga la vista
        /// </summary>
        private void LoadScreen()
        {
            Devices = DSconnection.DSConnection.GetDevicesList();
        }
        /// <summary>
        /// Metodo UpdateLayer, actuliza el estado de las capas de la consola
        /// </summary>
        private void UpdateLayer()
        {
            DSconnection.DSConnection.UpdateLayers(SLayerModel);
            _ea.GetEvent<EventLayers>().Publish(new EventsDataSet() { evento = "UpdateLayers" });
        }
        /// <summary>
        /// Metodo Cancel, termina la interación con la vista modal y la cierra 
        /// </summary>
        private void Cancel()
        {
            FinishInteraction?.Invoke();
        }
    }
}
