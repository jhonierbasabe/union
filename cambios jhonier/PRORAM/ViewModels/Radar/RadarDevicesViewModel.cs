using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using PRORAM.Models;
using Prism.Regions;
using PRORAM.Models.Shared;
using PRORAM.Models.TPC;
using PRORAM.Notifications;
using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using Microsoft.Maps.MapControl.WPF;
using System.Reflection;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

namespace PRORAM.ViewModels
{
    /// <summary>
    /// Clase RadarDevicesViewModel, contiene la logica y metodos que controlan la vista RadarDevices
    /// </summary>
    public class RadarDevicesViewModel : BindableBase, IInteractionRequestAware
    {
        #region private
        IEventAggregator _ea;
        public ObservableCollection<RadarDevicesModel> _RadarDevicesModel;
        private ObservableCollection<RadarDevicesModel> _RadarDevicesModel2;
        public ObservableCollection<string> ComboBoxOptions;
        private RadarConfigurationModel _RadarConfigurationModel;
        private string _tittle;
        private bool _resultOk;
        private bool _transResult;
        private bool _transResult2;
        private string _stateTrans;
        private string _messsageError;
        private string _action;
        private byte[] _buffer;
        private IRadarDevicesNotification _notification;
        private bool _respond;
        private RadarDevicesModel _SRadarDevicesModel;
        private string Timeejecution;
        private string _mensaje;
        private string _titulo;
        private int recoridor;
        private int radares;
        private int contador_radiacion = 0;
        private string RadarDesconectado;
        private bool conect = false;

        #endregion

        /// <summary>
        /// Propiedad Tittle, titulo
        /// </summary>
        public string Tittle
        {
            get { return _tittle; }
            set { _tittle = value; }
        }
        /// <summary>
        /// Propiedad SRadarDevicesModel, contiene informacion del radar seleccionado
        /// </summary>
        public RadarDevicesModel SRadarDevicesModel
        {
            get { return _SRadarDevicesModel; }
            set { SetProperty(ref _SRadarDevicesModel, value); }
        }


        /// <summary>
        /// Propiedad RadarConfigurationModel_ contiene la estructra de los datos de configuración
        /// </summary>
        public RadarConfigurationModel RadarConfigurationModel_
        {
            get { return _RadarConfigurationModel; }
            set { _RadarConfigurationModel = value; }
        }

        /// <summary>
        /// Propiedad RadarDevicesModel_, collección de RadarDevicesModel_, contiene los dispositivos radar agregados
        /// </summary>
        public ObservableCollection<RadarDevicesModel> RadarDevicesModel_
        {
            get { return _RadarDevicesModel; }
            set { SetProperty(ref _RadarDevicesModel, value); }
        }

        public ObservableCollection<RadarDevicesModel> RadarDevicesMode2_
        {
            get { return _RadarDevicesModel2; }
            set { SetProperty(ref _RadarDevicesModel2, value); }
        }

        
        /// <summary>
        /// Constructor RadarDevicesViewModel
        /// </summary>
        /// <param name="ea">captura los eventos provenientes de la vista</param>
        public RadarDevicesViewModel(IEventAggregator ea, object ea2, object ea1)
        {
            _ea = ea;
            _buffer = new byte[1];
            _buffer[0] = 0;
            _ea.GetEvent<MessageSentEvent<TransResult>>().Subscribe(CheckTrans);
            _ea.GetEvent<MsmSentEvent>().Subscribe(MonitoringConnection);

            _resultOk = false;
            _transResult = false;
            _transResult2 = false;

            RadarDevicesModel_ = DSconnection.DSConnection.GetDevicesList();
            SRadarDevicesModel = new RadarDevicesModel() { };
            RadarDevicesMode2_ = new ObservableCollection<RadarDevicesModel>();
            foreach (var device in RadarDevicesModel_)
            {
                device.Elevation = 0;
            }


            ConfirmationRequest = new InteractionRequest<IPopupConfirmationnotification>();
            ConfirmationCommand = new DelegateCommand(RaiseConfirmation);

            TXPowerRequest = new InteractionRequest<IPowerRadarNotification>();
            TXPowerCommand = new DelegateCommand(RaiseTXPower);

            RadarConfigurationNotificationRequest = new InteractionRequest<IRadarConfigurationNotification>();
            RadarconfigurationNotificationCommand = new DelegateCommand(RaiseRadarConfigurationInteraction);
            RadarconfigurationModo_2 = new DelegateCommand(RadarModo_2);
            //TextChangedCommand = new DelegateCommand(ObtenerordenRadar);

            RadiationCommand = new DelegateCommand(Radiation);

            GetIdRadarCommand = new DelegateCommand(GetIdRadar);
            GetIdRadarCheckboxCommand = new DelegateCommand(GetIdRadarCheckbox);

            CancelCommand = new DelegateCommand(Cancel);
            
            CustomPopupRequest2 = new InteractionRequest<INotification>();
            CustomPopupRequest = new InteractionRequest<INotification>();
            CustomPopupCommand = new DelegateCommand(RaiseCustomPopup);

            RaiseChannelFrecRequest = new InteractionRequest<IChannelFrecNotification>();
            RaiseChannelFrecCommand = new DelegateCommand(RaiseChannelFrec);

            ConsultDevicesCommand = new DelegateCommand(ConsultDevices);

            ConfigurationChangeNotificationRequest = new InteractionRequest<IRadarConfigurationNotification>();
            ConfigurationChangeCommand = new DelegateCommand(RaiseConfigurationChange);
        }
        /// <summary>
        /// Metodo MonitoringConnection, monitorea la conexión si se encuentra activa 
        /// </summary>
        /// <param name="obj"></param>
        private void MonitoringConnection(RadarActions obj)
        {
            if (obj.Action == "Disconnected")
            {
                int element = RadarDevicesModel_.IndexOf(RadarDevicesModel_.Where(x => x.GuidRadar == obj.GuidRadar).FirstOrDefault());
                RadarDevicesModel_[element].StateConnection = false;
                RadarDevicesModel_[element].Radiation = false;
                RadarDevicesModel_[element].IsSaveComplete = false;

                SRadarDevicesModel = RadarDevicesModel_[element];
                SRadarDevicesModel.IsSaveComplete = false;
                SRadarDevicesModel.StateConnection = false;
                SRadarDevicesModel.Radiation = false;
                SRadarDevicesModel.SaveProgressT = 0;
                SRadarDevicesModel.SaveProgress = 0;
                SRadarDevicesModel.Radiation = false;

                UpdateDevices(RadarDevicesModel_[element]);
            }

        }
        /// <summary>
        /// Metodo RaiseConfigurationChange, controla la interacción con la vista de edición de configuracion radar
        /// </summary>
        private void RaiseConfigurationChange()
        {
            var power = SRadarDevicesModel.TXPower;
            var element = RadarDevicesModel_.IndexOf(RadarDevicesModel_.Where(x => x.GuidRadar == SRadarDevicesModel.GuidRadar).FirstOrDefault());
            ConfigurationChangeNotificationRequest.Raise(new RadarConfigurationNotification
            {
                Title = "Configuración",
                RadarConfigurationModelI_ = new RadarConfigurationModel
                {
                    Id = SRadarDevicesModel.Id,
                    Altitude = SRadarDevicesModel.Altitude,
                    Azimuth = SRadarDevicesModel.Azimuth,
                    Elevation = SRadarDevicesModel.Elevation,
                    Guid = SRadarDevicesModel.GuidRadar,
                    InstallationAngle = SRadarDevicesModel.InstallationAngle,
                    IpAddress = SRadarDevicesModel.IpAddress,
                    IndexChannel = SRadarDevicesModel.SChannelFrec.Id,
                    Latitud = SRadarDevicesModel.Latitud,
                    Longitud = SRadarDevicesModel.Longitud,
                    Modelo = SRadarDevicesModel.IdModelo,
                    NorthHeiding = SRadarDevicesModel.NorthHeiding,
                    RadarName = SRadarDevicesModel.RadarName,
                    TXPower = SRadarDevicesModel.TXPower,
                    SModelo = new Modelo { Id = 1, Name = "ISYS-3106" },
                    SchannelFrec = SRadarDevicesModel.SChannelFrec
                }
            }, r =>
            {
                if (r.Confirmed)
                {
                    RadarDevicesModel_[element].TXPower = Convert.ToInt32(r.RadarConfigurationModelI_.TXPower);
                    RadarDevicesModel_[element].Altitude = r.RadarConfigurationModelI_.Altitude.Value;
                    RadarDevicesModel_[element].Azimuth = r.RadarConfigurationModelI_.Azimuth.Value;
                    RadarDevicesModel_[element].Latitud = r.RadarConfigurationModelI_.Latitud.Value;
                    RadarDevicesModel_[element].Longitud = r.RadarConfigurationModelI_.Longitud.Value;
                    RadarDevicesModel_[element].Elevation = r.RadarConfigurationModelI_.Elevation.Value;
                    RadarDevicesModel_[element].NorthHeiding = r.RadarConfigurationModelI_.NorthHeiding.Value;


                    UpdateDevices(RadarDevicesModel_[element]);
                    _ea.GetEvent<MessageSentEvent<object>>().Publish(new MessageConnect { GuidRadar = RadarDevicesModel_[element].GuidRadar, Actions = "UpdateConfiguration" });

                }
            });
        }


        /// <summary>
        /// Metodo RaiseChannelFrec, controla la interacción de la vista edicción de frecuencia
        /// </summary>
        private void RaiseChannelFrec()
        {
            var element = RadarDevicesModel_.IndexOf(RadarDevicesModel_.Where(x => x.GuidRadar == SRadarDevicesModel.GuidRadar).FirstOrDefault());
            RaiseChannelFrecRequest.Raise(new ChannelFrecNotification { Title = "Canales de frecuencia", IndexChannel = SRadarDevicesModel.SChannelFrec.Id, Device = SRadarDevicesModel.Id, ModeloS = SRadarDevicesModel.IdModelo }, r =>
                 {
                     if (r.Confirmed)
                     {

                         RadarDevicesModel_[element].SChannelFrec = r.Channelfrec;
//                         RadarDevicesModel_[element].
                         UpdateDevices(RadarDevicesModel_[element]);
                         if (RadarDevicesModel_[element].Radiation == true)
                         {

                             var messagesGenerator = new MessagesGenerator(1);
                             var messageToSend = messagesGenerator.GenerateMessageTXChannel((byte)RadarDevicesModel_[element].SChannelFrec.Id, RadarDevicesModel_[element].IdRadar);
                             ComunicationToGeoLayer(messageToSend, SRadarDevicesModel.GuidRadar, "MsmTXChannel");

                         }
                     }
                 });
        }
        /// <summary>
        /// Metodo RaiseTXPower, controla la iteracción de la vista edicion de potencia
        /// </summary>
        private void RaiseTXPower()
        {

            var power = SRadarDevicesModel.TXPower;
            var element = RadarDevicesModel_.IndexOf(RadarDevicesModel_.Where(x => x.GuidRadar == SRadarDevicesModel.GuidRadar).FirstOrDefault());

            TXPowerRequest.Raise(new PowerRadarNotification { Title = "Edicion potencia radar", TXPower = power, Device = element, GuidRadar = SRadarDevicesModel.GuidRadar }, r =>
             {
                 if (r.Confirmed)
                 {
                     var index = RadarDevicesModel_.IndexOf(RadarDevicesModel_.Where(x => x.GuidRadar == r.GuidRadar).FirstOrDefault());

                     RadarDevicesModel_[index].TXPower = r.TXPower;
                     UpdateDevices(RadarDevicesModel_[index]);

                     var messagesGenerator = new MessagesGenerator(1);
                     var messageToSend = messagesGenerator.GenerateMessageTXPower((byte)RadarDevicesModel_[index].TXPower, RadarDevicesModel_[index].IdRadar);
                     ComunicationToGeoLayer(messageToSend, SRadarDevicesModel.GuidRadar, "MsmTXPower");
                     UpdateDevices(SRadarDevicesModel);
                 }
             });
        }
        /// <summary>
        /// Metodo Cancel, finaliza la iteracción y cierra la vista modal
        /// </summary>
        private void Cancel()
        {
            foreach (var item in _RadarDevicesModel)
            {
                DSconnection.DSConnection.ModifyCheckRadarDeviceRow(item);
            }

            _notification.Confirmed = false;
            FinishInteraction?.Invoke();
        }
        /// <summary>
        /// Metodo Delete, remueve un dispositivo radar de la aplicacción
        /// </summary>
        private void Delete()
        {
            if (_respond)
            {
                var _guid = SRadarDevicesModel.GuidRadar;
                RemoveDevice(SRadarDevicesModel);
                _ea.GetEvent<MsmSentEvent>().Publish(new RadarActions { Action = "DeleteRadar", GuidRadar = _guid });
                RadarDevicesModel_.Remove(RadarDevicesModel_.Where(x => x.GuidRadar == _guid).Single());

                _respond = false;
            }

        }
        /// <summary>
        /// Metodo RaiseConfirmation, vista modal de notificación 
        /// </summary>
        private void RaiseConfirmation()
        {
            ConfirmationRequest.Raise(new PopupConfirmationnotification { Title = "Confirmation", Content = "Confirmation Message", Message = "¿Esta seguro que desea remover el dispositivo radar?" }, r => _respond = r.Confirmed);

            Delete();
        }


        #region Delegados
       

        public InteractionRequest<IPopupConfirmationnotification> ConfirmationRequest { get; set; }
        public DelegateCommand ConfirmationCommand { get; set; }

        /// <summary>
        /// Delegado DeleteCommand
        /// </summary>
        public DelegateCommand DeleteCommand { get; set; }

        /// <summary>
        /// Delegado SendMessageCommand
        /// </summary>
        public DelegateCommand SendMessageCommand { get; private set; }
        /// <summary>
        /// Propiedad RadarConfigurationNotificationRequest
        /// </summary>
        public InteractionRequest<IRadarConfigurationNotification> RadarConfigurationNotificationRequest { get; set; }
        /// <summary>
        /// Delegado RadarconfigurationNotificationCommand
        /// </summary>
        public DelegateCommand RadarconfigurationNotificationCommand { get; private set; }
        public DelegateCommand RadarconfigurationModo_2 { get; private set; }
        
        //public DelegateCommand TextChangedCommand { get; private set; }

        public InteractionRequest<IPowerRadarNotification> TXPowerRequest { get; set; }
        public InteractionRequest<INotification> CustomPopupRequest2 { get; set; }
        
        public InteractionRequest<IChannelFrecNotification> RaiseChannelFrecRequest { get; set; }

        public DelegateCommand RaiseChannelFrecCommand { get; set; }

        public DelegateCommand TXPowerCommand { get; set; }

        public DelegateCommand CustomPopupCommand { get; }

        public DelegateCommand RadiationCommand { get; set; }

        public InteractionRequest<INotification> CustomPopupRequest { get; set; }

        public InteractionRequest<INotification> NotificationRequest { get; set; }

        public DelegateCommand NotificationCommand { get; set; }

        public DelegateCommand CancelCommand { get; set; }

        public Action FinishInteraction { get; set; }

        public DelegateCommand GetIdRadarCommand { get; set; }
        public DelegateCommand GetIdRadarCheckboxCommand { get; set; }
        public InteractionRequest<IRadarConfigurationNotification> ConfigurationChangeNotificationRequest { get; set; }
        public DelegateCommand ConfigurationChangeCommand { get; set; }
        

        public INotification Notification

        {
            get { return _notification; }
            set { SetProperty(ref _notification, (IRadarDevicesNotification)value); }
        }

        public DelegateCommand ConsultDevicesCommand { get; set; }
        #endregion

        #region Metodos

        /// <summary>
        /// Metodo ConsultDevices, actualiza la propiedad RadarDevicesModel_ con los dispositivos actuales 
        /// </summary>
        private void ConsultDevices()
        {
            RadarDevicesModel_ = DSconnection.DSConnection.GetDevicesList();

        }

      


        /// <summary>
        /// Metodo Radiation: se encarga del control de animacion del boton y el envio del comando encender radiation
        /// </summary>
        private void Radiation()
        {
            if (SRadarDevicesModel.IsSaveCompleteT == true)
            {
                SRadarDevicesModel.IsSaveCompleteT = false;
                SRadarDevicesModel.SaveProgressT = 0;
                SRadarDevicesModel.Radiation = false;

                var messagesGenerator = new MessagesGenerator(1);
                var messageToSend = messagesGenerator.GenerateMessageEncender(0, SRadarDevicesModel.IdRadar);
                ComunicationToGeoLayer(messageToSend, SRadarDevicesModel.GuidRadar, "TurnOffRadiation");
                UpdateDevices(SRadarDevicesModel);
                return;
            }

            if (SRadarDevicesModel.SaveProgressT != 0) return;

            if (SRadarDevicesModel.StateConnection == true)
            {
                var started = DateTime.Now;

                SRadarDevicesModel.IsSavingT = true;

                var messagesGenerator = new MessagesGenerator(1);
                var messageToSend = messagesGenerator.GenerateMessageEncender(1, SRadarDevicesModel.IdRadar);

                ComunicationToGeoLayer(messageToSend, SRadarDevicesModel.GuidRadar, "TurnOnRadiation");

                new DispatcherTimer(
                    TimeSpan.FromMilliseconds(50),
                    DispatcherPriority.Normal,
                    new EventHandler((o, e) =>
                    {
                        var totalDuration = started.AddSeconds(3).Ticks - started.Ticks;
                        var currentProgress = DateTime.Now.Ticks - started.Ticks;
                        var currentProgressPercent = 100.0 / totalDuration * currentProgress;

                        SRadarDevicesModel.SaveProgressT = currentProgressPercent;

                        if (SRadarDevicesModel.SaveProgressT >= 100)
                        {

                            if (_transResult == false)
                            {
                                SRadarDevicesModel.Radiation = false;
                                SRadarDevicesModel.IsSaveCompleteT = false;
                                UpdateDevices(SRadarDevicesModel);
                            }
                            //if (_transResult == true && _buffer[0] == ListMessages.headerMessages["R_EncenderApagarRadar"])
                            if (_transResult == true && _action == "RadiationDone")
                            {
                                SRadarDevicesModel.Radiation = true;
                                SRadarDevicesModel.IsSaveCompleteT = true;
                                UpdateDevices(SRadarDevicesModel);
                                //if (_buffer[4] == 1)
                                //{
                                //    SRadarDevicesModel.Radiation = true;
                                //    SRadarDevicesModel.IsSaveCompleteT = true;
                                //    UpdateDevices(SRadarDevicesModel);
                                //}
                                //else
                                //{
                                //    SRadarDevicesModel.Radiation = false;
                                //    SRadarDevicesModel.IsSaveCompleteT = false;
                                //    UpdateDevices(SRadarDevicesModel);
                                //}
                            }

                            SRadarDevicesModel.IsSavingT = false;
                            SRadarDevicesModel.SaveProgressT = 0;
                            UpdateDevices(SRadarDevicesModel);
                            ((DispatcherTimer)o).Stop();
                        }

                    }), Dispatcher.CurrentDispatcher);
            }
        }

        private Visibility _contentControlVisibility;
        public Visibility ContentControlVisibility
        {
            get { return _contentControlVisibility; }
            set { SetProperty(ref _contentControlVisibility, value); }
        } 
        private void GetIdRadarCheckbox()
        {
            RadarDevicesModel radarToUpdate = RadarDevicesModel_.Where<RadarDevicesModel>(r => r.Id == SRadarDevicesModel.Id).FirstOrDefault();
            if (radarToUpdate != null)
            {
                radarToUpdate.Check = SRadarDevicesModel.Check;
            }

            if (SRadarDevicesModel.Check == true)
            {
                RadarDevicesModel radarToAdd = RadarDevicesMode2_.Where<RadarDevicesModel>(r => r.Id == SRadarDevicesModel.Id).FirstOrDefault();
                if (radarToAdd == null)
                {
                    RadarDevicesMode2_.Add(SRadarDevicesModel);

                }
            }
            else
            {
                RadarDevicesModel radarToRemove = RadarDevicesMode2_.Where<RadarDevicesModel>(r => r.Id == SRadarDevicesModel.Id).FirstOrDefault();
                if (radarToRemove != null)
                {
                    RadarDevicesMode2_.Remove(radarToRemove);
                }
            }
            ContentControlVisibility = RadarDevicesMode2_.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            ComboBoxOptions = new ObservableCollection<string>(RadarDevicesMode2_.Select(r => r.RadarName));
            
        }



        public string TiempoEjecucion
        {
            get { return Timeejecution; }
            set
            {
                Timeejecution = value;
                
            }
        }
        /// <summary>
        /// Metodo GetIdRadar: controla la animacion del boton y el envio del comando get id radar
        /// </summary>
        private void GetIdRadar()
        {
            if (SRadarDevicesModel.IsSaveComplete == true)
            {
                SRadarDevicesModel.IsSaveComplete = false;
                SRadarDevicesModel.StateConnection = false;
                SRadarDevicesModel.IsSaveCompleteT = false;
                SRadarDevicesModel.Radiation = false;
                SRadarDevicesModel.SaveProgressT = 0;
                SRadarDevicesModel.SaveProgress = 0;
                var messagesGenerator = new MessagesGenerator(1);
                var messageToSend = messagesGenerator.GenerateMessageEncender(0, 0);
                ComunicationToGeoLayer(messageToSend, SRadarDevicesModel.GuidRadar, "Disconnected");
                UpdateDevices(SRadarDevicesModel);
                return;
            }
            if (SRadarDevicesModel.SaveProgress != 0) return;
            SRadarDevicesModel.IsSaving = true;
            ComunicationToGeoLayer();
            var started = DateTime.Now;
            new DispatcherTimer(
                TimeSpan.FromMilliseconds(50),
                DispatcherPriority.Normal,
                new EventHandler((o, e) =>
                {
                    var totalDuration = started.AddSeconds(3).Ticks - started.Ticks;
                    var currentProgress = DateTime.Now.Ticks - started.Ticks;
                    var currentProgressPercent = 100.0 / totalDuration * currentProgress;

                    SRadarDevicesModel.SaveProgress = currentProgressPercent;

                    if (SRadarDevicesModel.SaveProgress >= 100)
                    {

                        if (_transResult == false)
                        {
                            SRadarDevicesModel.StateConnection = false;
                            SRadarDevicesModel.Radiation = false;
                            SRadarDevicesModel.IsSaveComplete = false;
                            SRadarDevicesModel.SaveProgress = 0;
                            UpdateDevices(SRadarDevicesModel);
                        }
                        if (_transResult == true && _stateTrans == "Success")
                        {
                            var header = Convert.ToInt32(ListMessages.headerMessages["R_IdRadar"]);
                            var buffer = Convert.ToInt32(_buffer[0]);

                            if (_buffer[4] != 0 && header == buffer)
                            {
                                SRadarDevicesModel.StateConnection = true;
                                SRadarDevicesModel.IsSaveComplete = true;
                                SRadarDevicesModel.IdRadar = _buffer[4];
                                SetIdRadar(_buffer[4]);
                                Console.WriteLine("El id de radar es " + _buffer[4]);
                                UpdateDevices(SRadarDevicesModel);

                            }
                            else
                            {
                                SRadarDevicesModel.IsSaveComplete = false;
                                SRadarDevicesModel.Radiation = false;
                                SRadarDevicesModel.StateConnection = false;
                                UpdateDevices(SRadarDevicesModel);

                            }
                        }

                        SRadarDevicesModel.IsSaving = false;
                        SRadarDevicesModel.SaveProgress = 0;
                        UpdateDevices(SRadarDevicesModel);
                        ((DispatcherTimer)o).Stop();
                        if (_transResult == false)
                        {
                            CustomPopupRequest2.Raise(new Notification { Title = "Notificación", Content = new { Text = _messsageError + SRadarDevicesModel.RadarName, Show = true, ShowAlert = true } }, r => Tittle = "PRORAM Consola de monitoreo");
                        }
                    }

                }), Dispatcher.CurrentDispatcher);


        }

        /// <summary>
        /// Metodo UpdateDevices, metodo que dispara el evento de actualizar el estado de un radar
        /// </summary>
        private void UpdateDevices(RadarDevicesModel obj)
        {
            DSconnection.DSConnection.UpdateDevice(obj);
            _ea.GetEvent<SendEventDataSet>().Publish(new EventsDataSet { evento = "GetDevicesList" });
        }

        /// <summary>
        /// Metodo que remueve un dispositivo radar del dataset 
        /// </summary>
        /// <param name="obj">radardevicemodel con la informacion del radar</param>
        private void RemoveDevice(RadarDevicesModel obj)
        {
            DSconnection.DSConnection.DeleteDeivice(obj);
            _ea.GetEvent<SendEventDataSet>().Publish(new EventsDataSet { evento = "GetDevicesList" });
        }
        /// <summary>
        /// SetIdaRadar, metodo que setea el id del radar a la collection
        /// </summary>
        /// <param name="id">id del radar</param>
        private void SetIdRadar(byte id)
        {
            SRadarDevicesModel.IdRadar = id;
            var element = RadarDevicesModel_.IndexOf(RadarDevicesModel_.Where(x => x.GuidRadar == SRadarDevicesModel.GuidRadar).FirstOrDefault());
            RadarDevicesModel_[element].IdRadar = id;
            SendAtionsRadar(new RadarActions()
            {
                Idradar = _buffer[4],
                GuidRadar = SRadarDevicesModel.GuidRadar,
                Action = "SetIdRadar"
            });

        }

        /// <summary>
        /// Metodo CheckTrans : recive el parametro de la transaccion del comando
        /// </summary>
        /// <param name="obj">objeto que contiene el resultado de la transaccion</param>
        private void CheckTrans(TransResult obj)
        {

            var result = obj as TransResult;
            _transResult = false;
            _transResult = result.Result;
            _buffer = result.Buffer;
            _stateTrans = result.State;
            _messsageError = result.Error;
            _action = result.Action;
            _resultOk = true;
            

        }

        private void RaiseCustomPopup()
        {
            var titulo = "Notificación";
            var mensaje = "Encienda el dispositivo antes de encender la radiación";
            CustomPopupRequest.Raise(new Notification { Title = titulo, Content = mensaje }, r => Tittle = "PRORAM Consola de monitoreo");
        }

        /// <summary>
        /// Metodo RadarconfigurationNotificationCommand, controla el llamado de la vista modal RadarConfiguration
        /// </summary>
        public void RaiseRadarConfigurationInteraction()
        {
            var count = RadarDevicesModel_.Count;
            var idRadar = count + 1;

            var p1 = _notification.Point1;
            var p2 = _notification.Point2;

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

        private void RadarModo_2()
        {
            
            if (Timeejecution == null)
            {
                CustomPopupRequest2.Raise(new Notification { Title = "Notificación", Content = new { Text = "Se debe ingregar el tiempo de operacion entre radares", Show = true, ShowAlert = true } }, r => Tittle = "PRORAM Consola de monitoreo");
                return;
            }
            foreach (var radarDevice in RadarDevicesMode2_)
            {
                if (radarDevice.Orden == 0)
                {
                    CustomPopupRequest2.Raise(new Notification { Title = "Notificación", Content = new { Text = "Se debe ingregar un orden para el radar" + radarDevice.RadarName, Show = true, ShowAlert = true } }, r => Tittle = "PRORAM Consola de monitoreo");
                    return;
                }
                if (radarDevice.Orden > RadarDevicesModel_.Count)
                {
                    CustomPopupRequest2.Raise(new Notification { Title = "Notificación", Content = new { Text = "El radar " + radarDevice.RadarName + " tiene un valor de orden superior a la cantidad de radares agregados", Show = true, ShowAlert = true } }, r => Tittle = "PRORAM Consola de monitoreo");
                    return;
                }
                if (radarDevice.Orden < 0)
                {
                    CustomPopupRequest2.Raise(new Notification { Title = "Notificación", Content = new { Text = "El radar " + radarDevice.RadarName + " tiene un valor de orden inferior a la cantidad minima de radares agregados", Show = true, ShowAlert = true } }, r => Tittle = "PRORAM Consola de monitoreo");
                    return;
                }

            }

            foreach (var radarDevice in RadarDevicesMode2_)
            {
                recoridor = radarDevice.Orden;
                radares = 0;
                foreach (var radarDevice_2 in RadarDevicesMode2_)
                {
                    if (recoridor == radarDevice_2.Orden)
                    {
                        radares++;
                    }
                    if (radares > 1)
                    {
                        CustomPopupRequest2.Raise(new Notification { Title = "Notificación", Content = new { Text = "El radar" + radarDevice.RadarName + " tiene el mismo orden que el radar " + radarDevice_2.RadarName, Show = true, ShowAlert = true } }, r => Tittle = "PRORAM Consola de monitoreo");
                        return;
                    }
                }
            }
            
            conect = !conect;
            conectar_radar();
        }
        

       public void conectar_radar()
        {
            var listaOrdenada = RadarDevicesMode2_.OrderBy(radarDevice => radarDevice.Orden).ToList();
            if (contador_radiacion < RadarDevicesMode2_.Count)
            {
                
                var radar = RadarDevicesModel_.Where(r => r.Id == listaOrdenada[contador_radiacion].Id).FirstOrDefault();
                if (radar != null)
                {
                    SRadarDevicesModel = radar;        
                    if (conect == true)
                    {
                        SRadarDevicesModel.IsSaving = true;
                        ComunicationToGeoLayer();
                        if (_transResult == false)
                        {
                            SRadarDevicesModel.StateConnection = false;
                            SRadarDevicesModel.Radiation = false;
                            SRadarDevicesModel.IsSaveComplete = false;
                            SRadarDevicesModel.SaveProgress = 0;
                            UpdateDevices(SRadarDevicesModel);
                            contador_radiacion++;
                            if (contador_radiacion >= RadarDevicesMode2_.Count)
                            {
                                contador_radiacion = 0;
                            }
                            if (conect)
                            {
                                conectar_radar();
                            }
                        }
                        if (_transResult == true && _stateTrans == "Success")
                        {
                            var header = Convert.ToInt32(ListMessages.headerMessages["R_IdRadar"]);
                            var buffer = Convert.ToInt32(_buffer[0]);

                            if (_buffer[4] != 0 && header == buffer)
                            {
                                SRadarDevicesModel.StateConnection = true;
                                SRadarDevicesModel.IsSaveComplete = true;
                                SRadarDevicesModel.IdRadar = _buffer[4];
                                SetIdRadar(_buffer[4]);
                                Console.WriteLine("El id de radar es " + _buffer[4]);
                                UpdateDevices(SRadarDevicesModel);
                                Radiation();
                                var started = DateTime.Now;
                                new DispatcherTimer(
                                    TimeSpan.FromMilliseconds(5000),
                                    DispatcherPriority.Normal,
                                    new EventHandler((o, e) =>
                                    {
                                        
                                        if (contador_radiacion < RadarDevicesMode2_.Count)
                                        {
                                            if (SRadarDevicesModel.IsSaveComplete == true)
                                            {
                                                Radiation();
                                                SRadarDevicesModel.IsSaveComplete = false;
                                                SRadarDevicesModel.StateConnection = false;
                                                SRadarDevicesModel.IsSaveCompleteT = false;
                                                SRadarDevicesModel.Radiation = false;
                                                SRadarDevicesModel.SaveProgressT = 0;
                                                SRadarDevicesModel.SaveProgress = 0;
                                                var messagesGenerator = new MessagesGenerator(1);
                                                var messageToSend = messagesGenerator.GenerateMessageEncender(0, 0);
                                                ComunicationToGeoLayer(messageToSend, SRadarDevicesModel.GuidRadar, "Disconnected");
                                                UpdateDevices(SRadarDevicesModel);

                                            }
                                            Console.WriteLine(contador_radiacion);
                                            contador_radiacion++;
                                            if (contador_radiacion >= RadarDevicesMode2_.Count)
                                            {
                                                contador_radiacion = 0;
                                            }
                                            if (conect)
                                            {
                                                conectar_radar();
                                            }
                                            
                                        }

                                        else
                                        {
                                            ((DispatcherTimer)o).Stop();


                                        }
                                    }), Dispatcher.CurrentDispatcher);

                            }
                            else
                            {
                                SRadarDevicesModel.IsSaveComplete = false;
                                SRadarDevicesModel.Radiation = false;
                                SRadarDevicesModel.StateConnection = false;
                                UpdateDevices(SRadarDevicesModel);

                            }
                            
                        }

                        SRadarDevicesModel.IsSaving = false;
                        UpdateDevices(SRadarDevicesModel);
                        
                        /*if (_transResult == false)
                        {
                            CustomPopupRequest2.Raise(new Notification { Title = "Notificación", Content = new { Text = _messsageError + SRadarDevicesModel.RadarName, Show = true, ShowAlert = true } }, r => Tittle = "PRORAM Consola de monitoreo");
                        }*/

                    }

                }



                

            }
        }
            


        /// <summary>
        /// Metodo ComunicationToGeoLayer, genera un evento que sera controlado en la vista GeoLayerViewModel
        /// </summary>
        private void ComunicationToGeoLayer()
        {
            _ea.GetEvent<MessageSentEvent<object>>().Publish(SRadarDevicesModel);

        }

       
        /// <summary>
        /// Metodo que envia el id que retorna el radar 
        /// </summary>
        /// <param name="radarActions">objeto con la informacion de las acciones</param>
        private void SendAtionsRadar(RadarActions radarActions)
        {

            _ea.GetEvent<MsmSentEvent>().Publish(radarActions);
        }

       
        
        /// <summary>
        /// Metodo ComunicationToGeoLayer, genera un evento que sera controlado en la vista GeoLayerViewModel
        /// </summary>
        /// <param name="data">información que se envia en el evento</param>
        /// <param name="guid">identificador unico global del radar</param>
        /// <param name="action">string con la acccion del evento</param>
        private void ComunicationToGeoLayer(byte[] data, Guid guid, string action)
        {
            _ea.GetEvent<MessageSentEvent<object>>().Publish(new MessageConnect { Data = data, GuidRadar = guid, Actions = action });
        }
        /// <summary>
        /// Metodo PushDevice, agrega otro dispositivo a la collección de radares
        /// </summary>
        /// 

      
        
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
        #endregion

       
       
        private bool _modo;

        [Obsolete]
        public bool Modo
        {
            get { return _modo; }
            set
            {
                _modo = value;
                Console.WriteLine("Modo seleccionado: " + value);
                OnPropertyChanged(nameof(Modo));
            }
        }

        public object TiempoOperacio { get; private set; }
    }

    





}
