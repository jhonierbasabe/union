using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using PRORAM.Models;
using PRORAM.Models.Shared;
using PRORAM.Models.TPC;
using PRORAM.Notifications;
using PRORAM.ResourcesFiles;
using System.Threading;
using PRORAM.ServicesTcp;

namespace PRORAM.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Propiedades privadas
        private string _title;
        private readonly IRegionManager _regionManager;
        private TargetAreaModel _targetAreaModel = new TargetAreaModel();
        private GeoLayerModel _geoLayerModel = new GeoLayerModel();
        private IEventAggregator _ea;
        private RadarDevicesModel _RadarDevicesModel;
        private string _mensaje;
        private string _titulo;
        private bool _saveDialog;
        private byte[] _messageReceived;
        private string _username = "aa";


        #endregion


        #region Propiedades
        public string load = "";

        /// <summary>
        /// Propiedad Message, array de byte que contiene le mensaje que se enviara al raadar
        /// </summary>
        public byte[] Message
        {
            get { return _messageReceived; }
            set { SetProperty(ref _messageReceived, value); }
        }

        /// <summary>
        /// Propiedad Title, titulo
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Propiedad RadarDevicesModel_, modelo de los dispositivos radares
        /// </summary>
        public RadarDevicesModel RadarDevicesModel_
        {
            get { return _RadarDevicesModel; }
            set { SetProperty(ref _RadarDevicesModel, value); }
        }
        /// <summary>
        /// Propiedad GeoLayerModel_, modelo del la capa geografica
        /// </summary>
        public GeoLayerModel GeoLayerModel_
        {
            get { return _geoLayerModel; }
            set { SetProperty(ref _geoLayerModel, value); }
        }
        /// <summary>
        /// Propiedad TargetAreaMod, modelo de area objetivo
        /// </summary>
        public TargetAreaModel TargetAreaMod
        {
            get { return _targetAreaModel; }
            set { SetProperty(ref _targetAreaModel, value); }
        }

        #endregion

        /// <summary>
        /// Constructor por defecto de la clase
        /// </summary>
        /// <param name="regionManager">parametro manejo region sobre la carga de vistas</param>
        /// <param name="ea">parametro de eventos</param>
        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            Title = "PRORAM Consola de monitoreo";
            _saveDialog = false;
            _regionManager = regionManager;
            GeoLayerModel_ = new GeoLayerModel();
            GeoLayerModel_.DefinedMap = false;
            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);

            TargetAreaNotificationRequest = new InteractionRequest<ITargetAreaNotification>();
            TargetAreaNotificationCommand = new DelegateCommand(RaiseTargetInteraction);

            RadarDevicesNotificationRequest = new InteractionRequest<IRadarDevicesNotification>();
            RadarDevicesNotificationCommand = new DelegateCommand(RaiseRadarInteraction);

            LayersNotificationRequest = new InteractionRequest<INotification>();

            //Linea Nueva
            MostrarParametros = new InteractionRequest<INotification>();

            //Linea nueva
            Parametros = new DelegateCommand(Fparametros);

            //Para abrir una pestaña
            LayersNotificationCommand = new DelegateCommand(RaiseLayersNotification);
            _ea = ea;

            _ea.GetEvent<MessageSentEvent<byte[]>>().Subscribe(ReceiveMessageToSend);

            DSconnection.DSConnection.AddLayers();
            _ea.GetEvent<MessageSentEvent>().Subscribe(LoadStageEvent);


            _ea.GetEvent<MsmSentEvent>().Subscribe(ActionsRadar);

            SaveCommand = new DelegateCommand(SaveStage);

            ConfirmationPopUpRequest = new InteractionRequest<IPopupConfirmationnotification>();
            ConfirmationCommand = new DelegateCommand(SaveStage);
            EventosCommand = new DelegateCommand<string>(EventosTool);
            LoadStageCommand = new DelegateCommand(LoadStage2);
        }


        #region Metodos conexión TCP


       
        private void ReceiveMessageToSend(byte[] message)
        {
            //SendCommand = new AsyncCommand(SendData, CanSend);
            Message = message;

            Console.WriteLine("Ejecuccion Comando");


            Console.WriteLine("SendCommand");
            // SendData();
            Message = null;
        }
        #endregion

        #region Metodos
        public void EventosTool(string typeEvent)
        {
            _ea.GetEvent<EventTools>().Publish(new ToolsSelect() { EventName = typeEvent });

        }
        /// <summary>
        /// Metodo ActionsRadar, recibe los eventos de tipo RadarActions
        /// </summary>
        /// <param name="obj">objeto de tipo RadarActions</param>
        private void ActionsRadar(RadarActions obj)
        {
            if (obj.Action == "Reset")
            {
                TargetAreaMod = new TargetAreaModel();
                GeoLayerModel_ = new GeoLayerModel();
            }
        }
        /// <summary>
        /// Metodo LoadStageEvent, se ejecuta cada vez que se carga la vista
        /// </summary>
        /// <param name="obj">objeto de tipo TargetAreaModel</param>
        private void LoadStageEvent(TargetAreaModel obj)
        {
            GeoLayerModel_.DefinedMap = true;
            TargetAreaMod = new TargetAreaModel { LatitudP1 = obj.LatitudP1, LatitudP2 = obj.LatitudP2, LongitudP1 = obj.LongitudP1, LongitudP2 = obj.LongitudP2, NombreArea = obj.NombreArea };
        }
        /// <summary>
        /// Metodo LoadStage2, carga una ventana que se encaga de notificar al momento de guardar un escenario
        /// </summary>
        private void LoadStage2()
        {

            if (DSconnection.DSConnection.CountDevices() > 0)
            {
                ConfirmationPopUpRequest.Raise(new PopupConfirmationnotification { Title = "Confirmation", Content = "Confirmation Message", Message = "Ya existe un escenario establecido,\n ¿Desea cargar un nuevo escenario?" }, r => _saveDialog = r.Confirmed);
                if (_saveDialog == true)
                {
                    DialogLoad();
                    



                }
            }
            else
            {
                DialogLoad();
                
            }
            
        }
        /// <summary>
        /// Metodo DialogLoad, abre una ventana de windows que permite cargar un escenario
        /// </summary>
        private void DialogLoad()
        {
         ////// carga la variable   string load = "";
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            //Pregunta si se escogio un archivo valido
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    //Carga el archivo txt en la variable load
                    load = File.ReadAllText(openFileDialog.FileName);

                    _ea.GetEvent<MsmSentEvent>().Publish(new RadarActions()
                    {
                        Action = "Reset"
                    });
                    DSconnection.DSConnection.Reset();

                    LoadStageModel stage = JsonConvert.DeserializeObject<LoadStageModel>(load);

                    DSconnection.DSConnection.AddSurveillanceArea(stage.SurveillanceArea.LatitudP1, stage.SurveillanceArea.LongitudP1, stage.SurveillanceArea.LatitudP2, stage.SurveillanceArea.LongitudP2, stage.SurveillanceArea.NombreArea);

                    _ea.GetEvent<MessageSentEvent>().Publish(new TargetAreaModel
                    {
                        LatitudP1 = stage.SurveillanceArea.LatitudP1,
                        LatitudP2 = stage.SurveillanceArea.LatitudP2,
                        LongitudP1 = stage.SurveillanceArea.LongitudP1,
                        LongitudP2 = stage.SurveillanceArea.LongitudP2,
                        NombreArea = stage.SurveillanceArea.NombreArea,
                        LogsEvent = "Se cargó un área de vigilancia."
                    });

                    foreach (var device in stage.Devices)
                    {
                        device.Radiation = false;
                        device.StateConnection = false;
                        device.IsSaveComplete = false;
                        device.IsSaveCompleteT = false;
                        device.SaveProgress = 0;
                        device.SaveProgressT = 0;
                        device.IsSaving = false;
                        device.IsSavingT = false;
                        DSconnection.DSConnection.AddRadarDeviceRow(device);
                        _ea.GetEvent<SendEventDataSet>().Publish(new EventsDataSet { evento = "GetDevicesList" });
                        _ea.GetEvent<MsmSentEvent>().Publish(new RadarActions()
                        {
                            GuidRadar = device.GuidRadar,
                            RadarDevice = device,
                            Action = "AddRadar",
                            Logs = "Se cargó un nuevo dispositivo radar con ip " + device.IpAddress
                        });

                    }
                }
                catch { }
            }
        }
        /// <summary>
        /// Metodo SaveStage, controla las acciones de guardado escenario
        /// </summary>
        private void SaveStage()
        {
            var devices = DSconnection.DSConnection.CountDevices();

            if (devices == 0)
            {
                System.Windows.Application.Current.Shutdown();
            }
            if (devices > 0)
            {
                _titulo = "Alerta";
                _mensaje = "Ya se ha definido un escenario seguro que quiere ";

                ConfirmationPopUpRequest.Raise(new PopupConfirmationnotification { Title = "Confirmation", Content = "Confirmation Message", Message = "Ya existe un escenario establecido,\n ¿Desea guarda el actual escenario antes de salir?" }, r => _saveDialog = r.Confirmed);
                if (_saveDialog == true)
                {
                    SaveDialog();
                }

                System.Windows.Application.Current.Shutdown();

            }

        }

        /// <summary>
        /// Metodo SaveDialog, abre una ventana que permite guardar un escenario
        /// </summary>
        private void SaveDialog()
        {
            string save = "";
            LoadStageModel stage = DSconnection.DSConnection.GetSurveillanceAreaModel();
            save = JsonConvert.SerializeObject(stage, Formatting.Indented);
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Text file(*.txt)| *.txt ";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, save);
            }
        }
        /// <summary>
        /// Metodo Navigate, controla las areas de carga de vistas
        /// </summary>
        /// <param name="navigatePath"></param>
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
        /// <summary>
        /// Metodo RaiseTargetInteraction, carga la vista modal de TargetAreaView
        /// </summary>
        private void RaiseTargetInteraction()
        {

            TargetAreaNotificationRequest.Raise(new TargetAreaNotification { Title = "Area objetivo" }, r =>
            {
                if (r.Confirmed && r.TargetAreaModel != null)
                {
                    TargetAreaMod = r.TargetAreaModel;
                    GeoLayerModel_.DefinedMap = true;
                    DSconnection.DSConnection.AddSurveillanceArea(TargetAreaMod.LatitudP1.Value, TargetAreaMod.LongitudP1.Value, TargetAreaMod.LatitudP2.Value, TargetAreaMod.LongitudP2.Value, TargetAreaMod.NombreArea);
                    SendMessage();
                }
                else
                {
                    Title = "PRORAM Consola de monitoreo";
                }
            });
        }



        /// <summary>
        /// Metodo RaiseRadarInteraction, controla las ventanas de modal de notificación
        /// </summary>
        private void RaiseRadarInteraction()
        {
            _titulo = "Mensaje de notificación ";
            _mensaje = "Necesita definir un área objetivo para poder agregar un dispositivo radar ";

            if (GeoLayerModel_.DefinedMap == true)
            {
                var p1 = new Location() { Latitude = TargetAreaMod.LatitudP1.Value, Longitude = TargetAreaMod.LongitudP1.Value };
                var p2 = new Location() { Latitude = TargetAreaMod.LatitudP2.Value, Longitude = TargetAreaMod.LongitudP2.Value };

                RadarDevicesNotificationRequest.Raise(new RadarDevicesNotification { Title = "Dispositivos radar", Point1 = p1, Point2 = p2 }, r =>
               {
                   if (r.Confirmed && r.RadarDevicesModel != null)
                   {
                       RadarDevicesModel_ = r.RadarDevicesModel;
                   }
                   else
                   {
                       Title = "PRORAM Consola de monitoreo";
                   }
               });
            }
            else
            {
                RaiseCustomPopup();
            }
            _titulo = string.Empty;
            _mensaje = string.Empty;
        }


        /// <summary>
        /// Metodo SendMessage, publica evento MessageSentEvent "Se agregó un area de vigilancia."
        /// </summary>
        private void SendMessage()
        {
            TargetAreaMod.LogsEvent = "Se agregó un area de vigilancia.";
            _ea.GetEvent<MessageSentEvent>().Publish(TargetAreaMod);
        }
        /// <summary>
        /// Metodo RaiseLayersNotification, controla la visualizacion de la vista modal LayersView
        /// </summary>
        private void RaiseLayersNotification()
        {
            LayersNotificationRequest.Raise(new Notification { Title = "Capas" }, r => Title = "PRORAM Consola de monitoreo");
        }

        private void Fparametros()
        {
            MostrarParametros.Raise(new Notification { Title = "Capas" }, r => Title = "PRORAM Consola de monitoreo");
        }
        /// <summary>
        /// Metodo RaiseCustomPopup, controla la visualización de la vista modal PopupConfirmationView
        /// </summary>
        private void RaiseCustomPopup()
        {
            CustomPopupRequest.Raise(new Notification { Title = _titulo, Content = new { Text = _mensaje, Show = false, ShowAlert = true } }, r => Title = "PRORAM Consola de monitoreo");
        }
        #endregion


        #region Delegate
        public InteractionRequest<INotification> LayersNotificationRequest { get; set; }
        //Linea Nueva
        public InteractionRequest<INotification> MostrarParametros { get; set; }
        public DelegateCommand LayersNotificationCommand { get; set; }
        //Linea nueva
        public DelegateCommand Parametros { get; set; }
        
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public InteractionRequest<INotification> CustomPopupRequest { get; set; }
        public DelegateCommand CustomPopupCommand { get; set; }

        public InteractionRequest<ITargetAreaNotification> TargetAreaNotificationRequest { get; set; }
        public DelegateCommand TargetAreaNotificationCommand { get; set; }

        public InteractionRequest<IRadarDevicesNotification> RadarDevicesNotificationRequest { get; set; }
        public DelegateCommand RadarDevicesNotificationCommand { get; set; }

        public DelegateCommand SendMessageCommand { get; private set; }

        public InteractionRequest<INotification> NotificationRequest { get; set; }
        public DelegateCommand NotificationCommand { get; set; }

        public ICommand ConnectCommand { get; set; }
        public ICommand DisconnectCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public DelegateCommand SaveCommand { get; }

        public InteractionRequest<IPopupConfirmationnotification> ConfirmationPopUpRequest { get; set; }
        public DelegateCommand ConfirmationCommand { get; set; }
        public DelegateCommand LoadStageCommand { get; set; }
        public DelegateCommand<string> EventosCommand { get; set; }

        #endregion Delegate        

    }

}
