using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PRORAM.Models;
using PRORAM.Models.Shared;
using PRORAM.Models.TPC;
using System.Device.Location;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
using GisLibrary;
using System.Text;
using System.Threading;
using System.Collections.Specialized;
using System.ComponentModel;
using PRORAM.Models.Layers;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Windows.Interop;
using System.Windows.Threading;
using PRORAM.ServicesTcp;
using Prism.Regions;

namespace PRORAM.ViewModels
{
    public class GeoLayerViewModel : BindableBase
    {
        #region private
        private LocationRect boundingRectangle;
        private GeoLayerModel _geoLayerModel = new GeoLayerModel();
        private LocationCollection _LocationCollection = new LocationCollection();
        private ObservableCollection<ServiceTcp> _servicesTcps;
        private IEventAggregator _ea;
        private const int _maxDevices = 10;

        private bool _layerPlots;
        private bool _layerTracks;
        private bool _layerDevices;
        private bool _layerClutter;
        private int _countClicks;
        private LocationCollection _locationsRule;
        private System.Windows.Forms.Timer _timerObjectlist;
        private ObservableCollection<RadarDevicesModel> _devices;
        private byte[] _messageReceived;
        



        #endregion

        #region propiedades
        /// <summary>
        /// Propiedad Devices, colección de dispositivos radar
        /// </summary>
        public ObservableCollection<RadarDevicesModel> Devices
        {
            get { return _devices; }
            set { SetProperty(ref _devices, value); }
        }
        /// <summary>
        /// Propiedad CollectionServicesTcp, coleccion de objetos de ServicesTcp que almacena la información de metodos de los servicios tcp 
        /// </summary>
        public ObservableCollection<ServiceTcp> CollectionServicesTcp
        {
            get { return _servicesTcps; }
            set { SetProperty(ref _servicesTcps, value); }
        }


        /// <summary>
        /// Propiedad GeoLayerModel, modelo de para la vista geolayer con las propiedades para el manejo de la vista
        /// </summary>
        public GeoLayerModel GeoLayerMod
        {
            get { return _geoLayerModel; }
            set { SetProperty(ref _geoLayerModel, value); }
        }
        /// <summary>
        /// Metodo MessageReceived, metodo controla  el evento de definición de área objetivo
        /// </summary>
        /// <param name="message">mensaje de tipo de modelo TargeAreaModel</param>
        private void MessageReceived(TargetAreaModel message)
        {

            GeoLayerMod.TargetAreaModel_ = message;
            SetArea();
        }
        #endregion


        /// <summary>
        /// Constructor por defecto
        /// </summary>
        /// <param name="ea">event aggregator</param>
        public GeoLayerViewModel(IEventAggregator ea)
        {
            GeoLayerMod = new GeoLayerModel();

            OnInitMap();
            GeoLayerMod.MyMap.Cursor = Cursors.Hand;

            _ea = ea;
            _ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
            _ea.GetEvent<MsmSentEvent>().Subscribe(ActionsRadar);

            _ea.GetEvent<MessageSentEvent<object>>().Subscribe(ConnectDevice);
            _ea.GetEvent<EventLayers>().Subscribe(ShowLayers);
            _ea.GetEvent<EventTools>().Subscribe(ToolsEvent);


            _layerClutter = false;
            _layerTracks = true;
            _layerPlots = true;


            _messageReceived = new byte[256];

            CollectionServicesTcp = new ObservableCollection<ServiceTcp>();
            CollectionServicesTcp.CollectionChanged += SubrcibreEvents;
            Devices = new ObservableCollection<RadarDevicesModel>();

            _countClicks = 0;
            FocusMapCommand = new DelegateCommand(FocusMap);
            ZoomInCommand = new DelegateCommand(ZoomIn);
            ZoomOutCommand = new DelegateCommand(ZoomOut);
            RotateCommand = new DelegateCommand(RotateView);
            MeasuringCommand = new DelegateCommand(Measuring);

            ClearRuleCommand = new DelegateCommand(ClearRule);

            _locationsRule = new LocationCollection();
            _timerObjectlist = new System.Windows.Forms.Timer();


        }

        #region Herramientas
        /// <summary>
        /// Metodo ToolsEvent, controla los eventos de las herramientas
        /// </summary>
        /// <param name="obj"></param>
        private void ToolsEvent(ToolsSelect obj)
        {
            if (obj.EventName == "Clear")
            {
                ClearRule();
            }

            if (obj.EventName == "Pan")
            {
                GeoLayerMod.MyMap.Cursor = Cursors.Hand;
            }

            if (obj.EventName == "Rotate")
            {
                RotateView();
            }

            if (obj.EventName == "Centrar")
            {
                FocusMap();
            }

            if (obj.EventName == "ZoomIn")
            {
                ZoomIn();
            }

            if (obj.EventName == "ZoomOut")
            {
                ZoomOut();
            }

            if (obj.EventName == "Rule")
            {
                Measuring();
            }
        }
        /// <summary>
        /// Metodo ClearRule, limpia las mediciones realizadas sobre la vista 
        /// </summary>
        private void ClearRule()
        {
            ClearScreen("Measuring", true);
        }

        /// <summary>
        /// Metodo RotateView, rota la vista 90 grados
        /// </summary>
        private void RotateView()
        {
            GeoLayerMod.MyMap.Cursor = Cursors.Hand;


            if (GeoLayerMod.MyMap.Heading < 360)
            {
                GeoLayerMod.MyMap.Heading += 90;
            }
            else
            {
                GeoLayerMod.MyMap.Heading = 0;
            }
        }
        /// <summary>
        /// Metodo ZoomOut, aleja la vista
        /// </summary>
        private void ZoomOut()
        {
            GeoLayerMod.MyMap.Cursor = Cursors.Hand;

            if (GeoLayerMod.DefinedMap == true)
            {
                if (GeoLayerMod.MyMap.ZoomLevel > 16)
                {
                    GeoLayerMod.MyMap.ZoomLevel -= 0.2;
                }
                else { GeoLayerMod.MyMap.ZoomLevel = 16; }
            }
            else { GeoLayerMod.MyMap.ZoomLevel -= 0.2; }

        }
        /// <summary>
        /// Metodo ZoomIn, acerca la vista
        /// </summary>
        private void ZoomIn()
        {
            GeoLayerMod.MyMap.Cursor = Cursors.Hand;

            if (GeoLayerMod.DefinedMap == true)
            {

                if (GeoLayerMod.MyMap.ZoomLevel < 19)
                {
                    GeoLayerMod.MyMap.ZoomLevel += 0.2;
                    ReDrawDots();
                    // ClearScreen("afasdf", false);
                }
                else { GeoLayerMod.MyMap.ZoomLevel = 19; }
            }
            else
            {
                GeoLayerMod.MyMap.ZoomLevel += 0.2;
            }

        }

        /// <summary>
        /// Metodo FocusMap, centra la vista sobre la área de vigilancia
        /// </summary>
        private void FocusMap()
        {
            GeoLayerMod.MyMap.Cursor = Cursors.Hand;


            if (DSconnection.DSConnection.CountDevices() > 0)
            {
                var area = (from r in GeoLayerMod.MyMap.Children.OfType<MapPolygon>()
                            where r.Tag == "GeoLayer"
                            select r).ToList();

                var X = (GeoLayerMod.TargetAreaModel_.LatitudP1.Value + GeoLayerMod.TargetAreaModel_.LatitudP2.Value) / 2;
                var Y = (GeoLayerMod.TargetAreaModel_.LongitudP1.Value + GeoLayerMod.TargetAreaModel_.LongitudP2.Value) / 2;

                GeoLayerMod.MyMap.Center = new Location(X, Y);
                GeoLayerMod.MyMap.ZoomLevel = 16;

                area[0].Focus();
            }
            GeoLayerMod.MyMap.Heading = 0;
            GeoLayerMod.MyMap.ZoomLevel = 16;

        }
        /// <summary>
        /// Metodo ShowLayers, metodo que controla los eventos de ocultar o mostrar capas
        /// </summary>
        /// <param name="obj">Objeto EventsDataSet contiene la inforacion de las capas</param>
        private void ShowLayers(EventsDataSet obj)
        {
            if (obj.evento == "UpdateLayers")
            {
                var layers = DSconnection.DSConnection.GetLayers();
                foreach (LayersModel b in layers)
                {
                    HiddenLayer(b.Sigla, b.State);
                }
            }
        }
        /// <summary>
        /// Metodo ReDrawDots, repinta los puntos de las lineas dibujadas de las mediciones
        /// </summary>
        private void ReDrawDots()
        {
            IList<Location> dotsPosicions = new List<Location>();

            var mapPolygons = from r in GeoLayerMod.MyMap.Children.OfType<System.Windows.Shapes.Ellipse>()
                              select r;
            var layers = mapPolygons.ToList();


            foreach (var l in layers)
            {
                var index = GeoLayerMod.MyMap.Children.IndexOf(l);
                var p0 = GeoLayerMod.MyMap.Children[index].TranslatePoint(new Point(0, 0), GeoLayerMod.MyMap);

                Point p1 = new Point(p0.X + 3, p0.Y + 3);
                Location loc = GeoLayerMod.MyMap.ViewportPointToLocation(p1);
                dotsPosicions.Add(loc);

            }

            ClearScreen("dots", false);

            foreach (var dots in dotsPosicions)
            {
                Ellipse dot = new Ellipse();
                dot.Fill = Brushes.Black;
                double radius = 3.0;
                dot.Width = radius * 2;
                dot.Uid = "dots";
                dot.Height = radius * 2;

                Point p0 = GeoLayerMod.MyMap.LocationToViewportPoint(dots);
                Point p1 = new Point(p0.X - radius, p0.Y - radius);
                Location loc = GeoLayerMod.MyMap.ViewportPointToLocation(p1);
                MapLayer.SetPosition(dot, loc);

                GeoLayerMod.MyMap.Children.Add(dot);

                GeoLayerMod.MyMap.UpdateLayout();
            }

        }

        /// <summary>
        /// Metodo que oculta capas del mapa
        /// </summary>
        /// <param name="tag">string con el tipo de tag de la capa que quieres ocultar</param>
        /// <param name="visibility">parametro que define si se muestra o oculta la capa</param>
        private void HiddenLayer(string tag, bool visibility)
        {
            var vs = (visibility == true) ? Visibility.Visible : Visibility.Hidden;

            if (tag == "Plots" || tag == "Track")
            {
                var mapPolygons = from r in GeoLayerMod.MyMap.Children.OfType<System.Windows.Shapes.Rectangle>()
                                  where r.Tag == tag
                                  select r;
                var layers = mapPolygons.ToList();
                if (tag == "Plots")
                {
                    _layerPlots = visibility;

                }
                else if (tag == "Track")
                {
                    _layerTracks = visibility;


                }
                foreach (var a in layers)
                {
                    var index = GeoLayerMod.MyMap.Children.IndexOf(a);
                    if (index != -1)
                    {
                        GeoLayerMod.MyMap.Children[index].Visibility = vs;
                    }
                }
            }
            if (tag == "Devices")
            {
                var mapLayers = from r in GeoLayerMod.MyMap.Children.OfType<MapLayer>()
                                where r.Tag == tag
                                select r;
                IList<MapLayer> layers = mapLayers.ToList();

                foreach (var layer in layers)
                {
                    var index = GeoLayerMod.MyMap.Children.IndexOf(layer);

                    if (index != -1)
                    {
                        GeoLayerMod.MyMap.Children[index].Visibility = vs;
                    }

                }
            }

            GeoLayerMod.MyMap.UpdateLayout();
        }

        /// <summary>
        /// Metodo que realiza las mediciones de sobre el mapa
        /// </summary>
        private void Measuring()
        {
            GeoLayerMod.MyMap.Cursor = Cursors.Arrow;

            if (_countClicks < 2)
            {
                GeoLayerMod.MyMap.MouseDown += CursorTypeChanged;
            }


        }

        /// <summary>
        /// Metodo que controla los cambios de cursor
        /// </summary>
        /// <param name="sender">objeto que ejecuta el evento</param>
        /// <param name="e">parametro de mouse button event argument</param>
        private void CursorTypeChanged(object sender, MouseButtonEventArgs e)
        {

            Point mousePosition = e.GetPosition(GeoLayerMod.MyMap);
            Location center = GeoLayerMod.MyMap.ViewportPointToLocation(mousePosition);


            if (_locationsRule.Count < 2)
            {
                var location = new Location() { Latitude = center.Latitude, Longitude = center.Longitude };


                //_locationsRule.Add(location);

                Ellipse dot = new Ellipse();
                dot.Fill = Brushes.Black;
                double radius = 3.0;
                dot.Width = radius * 2;
                dot.Uid = "dots";
                dot.Height = radius * 2;

                Point p0 = GeoLayerMod.MyMap.LocationToViewportPoint(location);
                Point p1 = new Point(p0.X, p0.Y);
                Point p2 = new Point(p0.X + 0.5, p0.Y + 0.5);
                _locationsRule.Add(GeoLayerMod.MyMap.ViewportPointToLocation(p2));
                Location loc = GeoLayerMod.MyMap.ViewportPointToLocation(p1);
                MapLayer.SetPosition(dot, loc);

                //_locationsRule.Add(loc);
                GeoLayerMod.MyMap.Children.Add(dot);

                if (_locationsRule.Count() == 2)
                {
                    DrawLine();
                    GeoLayerMod.MyMap.Cursor = Cursors.Hand;
                }
                GeoLayerMod.MyMap.UpdateLayout();
            }


            if (_countClicks == 2)
            {

                GeoLayerMod.MyMap.Cursor = Cursors.Hand;
                GeoLayerMod.MyMap.MouseDown -= CursorTypeChanged;
                _countClicks = 0;
                _locationsRule = new LocationCollection();
            }
            _countClicks += 1;

        }
        /// <summary>
        /// Metodo DrawLine, dibuja una linea recta para las mediciones sobre la vista geografica
        /// </summary>
        private void DrawLine()
        {

            var gis = new GisLibrary.Proj4();
            double distance = Math.Abs((gis.ConvertToMeter(_locationsRule[0].Latitude, _locationsRule[0].Longitude, _locationsRule[1].Latitude, _locationsRule[1].Longitude)));

            MapPolyline lineRule = new MapPolyline();
            TextBlock label = new TextBlock();
            label.Text = string.Format("Distancia: {0} m ", distance);
            label.TextAlignment = TextAlignment.Center;
            label.Tag = "Measuring";

            double dy = _locationsRule[1].Longitude - _locationsRule[0].Longitude;
            double dx = _locationsRule[1].Latitude - _locationsRule[0].Latitude;
            double theta = Math.Atan2(dy, dx);
            theta *= 180 / Math.PI;
            var TransformAngle = theta - 90;
            label.LayoutTransform = new RotateTransform(TransformAngle);
            var labelPosicion = gis.CalculateDistanceLine(_locationsRule[0].Latitude, _locationsRule[0].Longitude, _locationsRule[1].Latitude, _locationsRule[1].Longitude);

            Location loc = new Location() { Latitude = labelPosicion.Lat, Longitude = labelPosicion.Lon };
            MapLayer.SetPosition(label, loc);

            lineRule.Locations = _locationsRule;
            lineRule.Tag = "Measuring";
            lineRule.StrokeThickness = 2;
            lineRule.Stroke = Brushes.Black;
            var toolTip = new ToolTip();

            toolTip.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            toolTip.Content = string.Format("Distancia: {0} m ", distance);
            lineRule.ToolTip = toolTip;

            GeoLayerMod.MyMap.Children.Add(lineRule);
            GeoLayerMod.MyMap.Children.Add(label);
        }


        #endregion



        #region Metodos TCP
        /// <summary>
        /// Metodo ActionsRadar, controla los eventos sobre las acciones de los dipositivos radar
        /// </summary>
        /// <param name="obj"></param>
        private void ActionsRadar(RadarActions obj)
        {

            var element = CollectionServicesTcp.IndexOf(CollectionServicesTcp.Where(x => x.GuidRadar == obj.GuidRadar).FirstOrDefault());

            if (obj.Action == "UpdateConfiguration")
            {
                var aa = GeoLayerMod.MyMap.Children;
                var mapLayers = from r in GeoLayerMod.MyMap.Children.OfType<MapLayer>()
                                where r.Uid == CollectionServicesTcp[element].GuidRadar.ToString()
                                select r;
                var layers = mapLayers.ToList();

                var cones = from r in GeoLayerMod.MyMap.Children.OfType<MapPolygon>()
                            where r.Uid == CollectionServicesTcp[element].GuidRadar.ToString()
                            select r;
                var cone = cones.ToList();

                GeoLayerMod.MyMap.Children.Remove(layers[0]);
                GeoLayerMod.MyMap.Children.Remove(cone[0]);
                GeoLayerMod.MyMap.UpdateLayout();

                AddDevice(obj.RadarDevice, true);
            }
            if (obj.Action == "Reset")
            {

                GeoLayerMod.MyMap.Children.Clear();
                foreach (var r in CollectionServicesTcp)
                {
                    r.DisconnectedRadar();
                }


            }
            //if (obj.Action == "SetIdRadar")
            //{

            //    if (element != -1)
            //    {
            //        TCPClient_[element].RadarDevices.IdRadar = obj.Idradar;

            //    }
            //}
            //Agrega dispositivo radar a la colleccion
            if (obj.Action == "AddRadar")
            {

                if (element == -1)
                {
                    AddDevice(obj.RadarDevice);
                }
            }
            if (obj.Action == "DeleteRadar")
            {
                var aa = GeoLayerMod.MyMap.Children;
                var mapLayers = from r in GeoLayerMod.MyMap.Children.OfType<MapLayer>()
                                where r.Uid == CollectionServicesTcp[element].GuidRadar.ToString()
                                select r;
                var layers = mapLayers.ToList();

                var cones = from r in GeoLayerMod.MyMap.Children.OfType<MapPolygon>()
                            where r.Uid == CollectionServicesTcp[element].GuidRadar.ToString()
                            select r;
                var cone = cones.ToList();

                GeoLayerMod.MyMap.Children.Remove(layers[0]);
                GeoLayerMod.MyMap.Children.Remove(cone[0]);
                GeoLayerMod.MyMap.UpdateLayout();

                //GeoLayerMod.MyMap.Children.Remove(recs);
                try
                {
                    CollectionServicesTcp.Remove(CollectionServicesTcp.Where(x => x.GuidRadar == obj.GuidRadar).Single());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }

        }
        /// <summary>
        /// Metodo ConnectDevice rutina de mensajes entre la consola y el dispositivo radar con el fin establecer la conexión
        /// </summary>
        /// <param name="obj">objecto generico contiene la información de los mensajes y dispositivo a conectar</param>
        private void ConnectDevice(object obj)
        {
            int element = 0;

            if (obj is RadarDevicesModel)
            {
                var device = obj as RadarDevicesModel;

                element = CollectionServicesTcp.IndexOf(CollectionServicesTcp.Where(x => x.GuidRadar == device.GuidRadar).FirstOrDefault());

                if (element != -1)
                {
                    bool step = false;
                    byte idRadar = 0;
                    byte[] ResultGetIdRadar = new byte[1024];
                    bool isRunning = false;
                    byte[] result = new byte[1024];
                    MessagesGenerator messagesGenerator = new MessagesGenerator(1);
                    byte[] messageToSend = messagesGenerator.GenerateMessageIdRadar(1);
                    string erroCode = "";

                    if (CollectionServicesTcp[element].IsConnected == false)
                    {
                        CollectionServicesTcp[element].StartClient();
                    }
                    if (CollectionServicesTcp[element].IsConnected == false)
                    {
                        step = false;
                    }

                    var msmGenerator = new MessagesGenerator(1);
                    var msmTournOff = msmGenerator.GenerateMessageEncender(0, 85);

                    Thread.Sleep(100);
                    if (CollectionServicesTcp[element].IsConnected == true)
                    {
                        CollectionServicesTcp[element].SendAndReceive(msmTournOff, 49);
                        //ResultGetIdRadar = result;
                        step = true;
                    }
                    else
                    {
                        step = false;
                    }

                    Thread.Sleep(100);
                    if (CollectionServicesTcp[element].IsConnected == true && step == true)
                    {
                        result = CollectionServicesTcp[element].SendAndReceive(messageToSend);
                        ResultGetIdRadar = result;
                        idRadar = result[4];
                    }
                    else
                    {
                        erroCode = "\nFallo la conexión ";
                        step = false;
                    }

                    Thread.Sleep(100);
                    if (result[0] == ListMessages.headerMessages["R_IdRadar"] && step == true)
                    {
                        Console.WriteLine("R_IdRadar header = " + result[0]);
                        messagesGenerator = new MessagesGenerator(4);
                        messageToSend = messagesGenerator.GenerateMessageTime(idRadar);
                        result = CollectionServicesTcp[element].SendAndReceive(messageToSend);

                    }
                    else
                    {
                        step = false;
                    }


                    Thread.Sleep(100);
                    if (result[0] == ListMessages.headerMessages["RC_Hora"] && step == true)
                    {
                        Console.WriteLine("R_GetStatus header = " + result[0]);
                        messagesGenerator = new MessagesGenerator(1);
                        messageToSend = messagesGenerator.GenerateMessageTXPower((byte)device.TXPower, idRadar);
                        result = CollectionServicesTcp[element].SendAndReceive(messageToSend);


                    }
                    else if (erroCode == "")
                    {
                        erroCode = "\nFallo en el mensaje de SetTimeStamp ";
                        step = false;
                    }

                    Thread.Sleep(100);
                    if (result[0] == ListMessages.headerMessages["RC_PotenciaRadar"] && step == true)
                    {
                        Console.WriteLine("RC_PotenciaRadar header = " + result[0]);
                        messagesGenerator = new MessagesGenerator(1);
                        messageToSend = messagesGenerator.GenerateMessageTXChannel((byte)device.SChannelFrec.Id, idRadar);
                        result = CollectionServicesTcp[element].SendAndReceive(messageToSend);

                    }
                    else if (erroCode == "")
                    {
                        erroCode = "\nFallo en el mensaje de SetTxPower ";
                        step = false;
                    }

                    Thread.Sleep(100);
                    if (result[0] == ListMessages.headerMessages["RC_CanalFrecRadar"] && step == true)
                    {

                        Console.WriteLine("Conexion realizada satisfactoriamente ConnectDone");
                        CollectionServicesTcp[element]._timerGetStatus.Enabled = true;

                        PublishResult(ResultGetIdRadar, "Success", step, "ConnectDone");

                    }
                    else if (erroCode == "")
                    {
                        erroCode = "\nFallo en el mensaje de SetTxChannel ";
                        step = false;
                    }

                    if (step == false)
                    {
                        step = false;
                        string mensajeError = "No se puede conectar el dispositivo: " + erroCode;
                        CollectionServicesTcp[element].DisconnectedRadar();
                        PublishResult("Fail ", false, "Error", mensajeError);
                    }

                }
            }
            else if (obj is MessageConnect)
            {
                byte[] result = new byte[1024];
                MessageConnect msm = obj as MessageConnect;
                element = CollectionServicesTcp.IndexOf(CollectionServicesTcp.Where(x => x.GuidRadar == msm.GuidRadar).FirstOrDefault());


                bool step = false;



                //if(result[0]==ListMessages.headerMessages[""] && msm.Actions=="")
                Thread.Sleep(100);
                if (msm.Actions == "Disconnected")
                {
                    CollectionServicesTcp[element].DisconnectedRadar();
                }
                if (msm.Actions == "TurnOnRadiation")
                {
                    result = CollectionServicesTcp[element].SendAndReceive(msm.Data);
                    PublishResult(result, "Success", true, "RadiationDone");
                    CollectionServicesTcp[element].StartRunning();
                    Task.Run(() => CollectionServicesTcp[element].UpdateConnection());
                    step = true;
                    //PublishResult(result, "Success", step, "ConnectDone");
                    //                    private void PublishResult(byte[] respond, string state, bool result, string action)

                }
                if (msm.Actions == "TurnOffRadiation")
                {
                    result = CollectionServicesTcp[element].SendAndReceive(msm.Data);
                    PublishResult(result, "Success", true, "RadiationDone");
                    CollectionServicesTcp[element].StopRunning();
                    /// Task.Run(() => CollectionServicesTcp[element].UpdateConnection());
                    step = true;
                }
                if (msm.Actions == "MsmTXPower" || msm.Actions == "MsmTXChannel")
                {
                    var devices = DSconnection.DSConnection.GetDevicesList();
                    var device = devices.Where(x => x.GuidRadar == msm.GuidRadar).FirstOrDefault();

                    if (msm.Actions == "MsmTXChannel")
                    {

                        CollectionServicesTcp[element].Device.SChannelFrec = device.SChannelFrec;
                    }
                    if (msm.Actions == "MsmTXPower")
                    {
                        CollectionServicesTcp[element].Device.TXPower = device.TXPower;
                    }
                    if (CollectionServicesTcp[element].IsConnected == true && CollectionServicesTcp[element]._isRunning == true)
                    {
                        CollectionServicesTcp[element].sendData(msm.Data);
                    }
                }
                if (msm.Actions == "UpdateConfiguration")
                {
                    var devices = DSconnection.DSConnection.GetDevicesList();
                    var device = devices.Where(x => x.GuidRadar == msm.GuidRadar).FirstOrDefault();
                    
                    CollectionServicesTcp[element].Device.SChannelFrec = device.SChannelFrec;
                    CollectionServicesTcp[element].Device.TXPower = device.TXPower;
                    
                    var msmGenerator = new MessagesGenerator(1);
                    var msmUpdate =msmGenerator.GenerateMessageTXChannel((byte)device.SChannelFrec.Id, device.IdRadar);
        
                    if (CollectionServicesTcp[element].IsConnected == true && CollectionServicesTcp[element]._isRunning == true)
                    {
                        CollectionServicesTcp[element].sendData(msmUpdate);
                    }
        
                    Thread.Sleep(100);
                    msmUpdate = msmGenerator.GenerateMessageTXPower((byte)device.TXPower, device.IdRadar);
                    if (CollectionServicesTcp[element].IsConnected == true && CollectionServicesTcp[element]._isRunning == true)
                    {
                        CollectionServicesTcp[element].sendData(msmUpdate);
                    }

                    Thread.Sleep(100);
                }

            }

        }

        /// <summary>
        /// Metodo que publica mensajes del los resultados de la transccion de los mensajes entre la consola y los dispositivos radar
        /// </summary>
        /// <param name="state">cadena de textodefinie si es succes o fail la transaccion</param>
        /// <param name="result">bool define si el resultado es correcto</param>
        /// <param name="action">cadena de textocon la accion del evento</param>
        /// <param name="error">cadena de texto de un posible errro</param>
        private void PublishResult(string state, bool result, string action, string error)
        {
            _ea.GetEvent<MessageSentEvent<TransResult>>().Publish(new TransResult
            {
                Buffer = _messageReceived,
                Result = result,
                State = state,
                Error = error,
                Action = action
            });
        }
        /// <summary>
        ///  Metodo que publica mensajes del los resultados de la transccion de los mensajes entre la consola y los dispositivos radar
        /// </summary>
        /// <param name="respond">array de bytes mensaje de respuesta del dispositivo radar</param>
        /// <param name="state">cadena de textodefinie si es succes o fail la transaccion</param>
        /// <param name="result">bool define si el resultado es correcto</param>
        /// <param name="action">cadena de texto de un posible errro</param>
        private void PublishResult(byte[] respond, string state, bool result, string action)
        {
            _ea.GetEvent<MessageSentEvent<TransResult>>().Publish(new TransResult { Buffer = respond, Result = result, State = state, Action = action, Error = "" });
        }


        #endregion



        #region Delegados

        public DelegateCommand FocusMapCommand { get; set; }
        public DelegateCommand ZoomInCommand { get; set; }
        public DelegateCommand ZoomOutCommand { get; set; }
        public DelegateCommand RotateCommand { get; set; }
        public DelegateCommand MeasuringCommand { get; set; }
        public DelegateCommand ClearRuleCommand { get; set; }
        public bool Azimuth { get; private set; }
        public object targetPanelViewModel { get; private set; }


        #endregion Delegados

        #region Manejo de cordenadas
        /// <summary>
        /// Metodo SubrcibreEvents, subcribe metodos a la coleccion de plots y tracks
        /// </summary>
        /// <param name="sender">objeto que genera el llamando del evento</param>
        /// <param name="e">argumento de cambio de coleccion</param>
        private void SubrcibreEvents(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ServiceTcp tcp in e.NewItems)
                {
                    tcp.PlotsFromRadar.CollectionChanged += new NotifyCollectionChangedEventHandler((s, g) => DrawinPlots(s, g, tcp));
                    //+= new NotifyCollectionChangedEventHandler((s, g) => DrawingPlots(s, g, tcp));
                    tcp.TracksFromRadar.CollectionChanged += new NotifyCollectionChangedEventHandler((s, g) => DrawingTracks(s, g, tcp));
                    tcp.RadarDisconnection += MonitoringConnection;
                }
            }
            if (e.OldItems != null)
            {
                foreach (ServiceTcp tcp in e.OldItems)
                {
                    tcp.PlotsFromRadar.CollectionChanged += new NotifyCollectionChangedEventHandler((s, g) => DrawinPlots(s, g, tcp));
                    tcp.TracksFromRadar.CollectionChanged -= new NotifyCollectionChangedEventHandler((s, g) => DrawingTracks(s, g, tcp));
                    tcp.RadarDisconnection -= MonitoringConnection;
                }
            }
        }
        /// <summary>
        /// evento que realiza las acciones sobre los plots borrar y dibujar 
        /// </summary>
        /// <param name="s">objeto sender que activa el eveto</param>
        /// <param name="g">argumento NotifyCollectionChangedEventArgs</param>
        /// <param name="tcp">objeto de comunicación TCP</param>
        private void DrawinPlots(object s, NotifyCollectionChangedEventArgs g, ServiceTcp tcp)
        {
            if (g.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (Plots plts in g.OldItems)
                {
                    string uid = plts.PlotGuid.ToString() + "-" + plts.RadarId;
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ClearScreenPlots(uid);
                    }), DispatcherPriority.Background);
                }
            }
            if (g.Action == NotifyCollectionChangedAction.Add)
            {
                if (tcp.PlotsFromRadar.Count > 0)
                {
                    DrawPlots(tcp.PlotsFromRadar.Last(), tcp.Device, true);
                }
            }
            if (g.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Plots plts in g.OldItems)
                {
                    string uid = plts.PlotGuid.ToString() + "-" + plts.RadarId;
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ClearScreenPlots(uid);
                    }));
                }
            }
        }
        /// <summary>
        /// Evento MonitoringConnection, se activa al perder la conexión con el radar
        /// </summary>
        /// <param name="IsDisconnected">Bandera de activacion</param>
        /// <param name="Device">dispositivo radar</param>
        private void MonitoringConnection(bool IsDisconnected, RadarDevicesModel Device)
        {
            Console.WriteLine("Evento de desconexión");
            App.Current.Dispatcher.Invoke(delegate
            {
                _ea.GetEvent<MsmSentEvent>().Publish(new RadarActions()
                {
                    Action = "Disconnected",
                    GuidRadar = Device.GuidRadar,
                    Idradar = Device.IdRadar,
                    Logs = string.Format("Se Ha desconectado el radar {0}, con ip {1}", Device.RadarName, Device.IpAddress)
                });
            });
        }



        /// <summary>
        /// Metodo que captura los eventos realizados al añadir un nuevo plot
        /// </summary>
        /// <param name="plt">objeto de tipo Plot</param>
        /// <param name="action">cadena de texto con la accion realizada sobre el plot</param>
        /// <param name="device">dispositivo radar al que pertene el plot</param>
        private void Tcp_ActionsPlot(Plots plt, string action, RadarDevicesModel device)
        {
            if (action == "OnAddPlot")
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    // var mapPolygon = new MapPolygon();
                    DrawPlots(plt, device, true);
                }), DispatcherPriority.Background);

            }
            if (action == "OnDeletePlot")
            {
                string uid = plt.PlotGuid.ToString() + "-" + plt.RadarId;
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ClearScreenPlots(uid);
                    //ClearScreen("Plots", uid);
                }));
            }
        }
        /// <summary>
        /// Metodo que captura el evento de agregar o borrar  Track's sobre la consola
        /// </summary>
        /// <param name="sender">objeto que invoca el evento</param>
        /// <param name="e">argumento cambio de coleccion</param>
        /// <param name="tcp">objeto ServiceTcp que controla la conexion con dispositivo radar</param>
        private void DrawingTracks(object sender, NotifyCollectionChangedEventArgs e, ServiceTcp tcp)
        {

            if (tcp._isRunning == true && e.Action == NotifyCollectionChangedAction.Add)
            {
                var element = Devices.IndexOf(Devices.Where(x => x.GuidRadar == tcp.GuidRadar).FirstOrDefault());
                this.DrawTracks(tcp.TracksFromRadar.Last(), Devices[element]);
                var trck = tcp.TracksFromRadar.Last();
                string uid = trck.Id + "-" + tcp.GuidRadar;
                _ea.GetEvent<EventPanel>().Publish(new DetailPanel { Track = trck, Action = "Add", Target = "Track", Guid_ = uid });

            }
            if (tcp._isRunning == true && e.Action == NotifyCollectionChangedAction.Replace)
            {
                var element = Devices.IndexOf(Devices.Where(x => x.GuidRadar == tcp.GuidRadar).FirstOrDefault());
                var index = e.NewStartingIndex;
                string uid = tcp.TracksFromRadar[index].Id + "-" + tcp.GuidRadar;
                App.Current.Dispatcher.Invoke(delegate
                {
                    return ClearScreen("Track", uid);
                });
                this.DrawTracks(tcp.TracksFromRadar[index], Devices[element]);

                _ea.GetEvent<EventPanel>().Publish(new DetailPanel { Track = tcp.TracksFromRadar[index], Action = "Replace", Target = "Track", Guid_ = uid });
            }
            if (tcp._isRunning == true && e.Action == NotifyCollectionChangedAction.Remove)
            {
                var es = e.OldItems;
                foreach (Tracks trck in e.OldItems)
                {
                    string uid = trck.Id + "-" + tcp.GuidRadar;
                    _ea.GetEvent<EventPanel>().Publish(new DetailPanel { Guid_ = uid, Action = "Clear", Target = "Track", Track = trck });
                    App.Current.Dispatcher.Invoke(delegate
                    {
                        return ClearScreen("Track", uid);
                    });

                }
            }
        }
        /// <summary>
        /// Metodo que dibuja Tracks agregado a la consola
        /// </summary>
        /// <param name="tracks">Objeto de tipo Track</param>
        /// <param name="device">dispositivo radar al que pertenece el Track</param>
        private void DrawTracks(Tracks tracks, RadarDevicesModel device)
        {
            var gis = new GisLibrary.Proj4();
            var latitud = device.Latitud;
            var longitud = device.Longitud;
            //public IList<Posicion> ConvertionTracks(double latitud, double longitud, double teta, double pAngle, double PosX, double PosY, int type)           
            var points = gis.ConvertionTracks(latitud, longitud, device.NorthHeiding, tracks.Azimuth, tracks.PosX, tracks.PosY, tracks.Type);

            var t = DrawPolygon(points, Brushes.Red);
            var toolTip = new ToolTip();
            toolTip.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            toolTip.Content = string.Format("Id: {0} \nDistancia: {1} m \nVelocidad: {0} Km/h", tracks.Id, tracks.DistanceToRadar, tracks.Velocity);
            Console.WriteLine(toolTip.Content);

            t.ToolTip = toolTip;

            t.Tag = "Track";
            t.Uid = Convert.ToString(tracks.Id) + "-" + device.GuidRadar.ToString();
            var _uid = Convert.ToString(tracks.Id) + "-" + device.GuidRadar.ToString();
            t.MouseDown += new MouseButtonEventHandler((s, g) => OnTrackDetail(s, g, tracks, _uid));

            this.GeoLayerMod.MyMap.Children.Add(t);
            this.GeoLayerMod.MyMap.UpdateLayout();


        }
        /// <summary>
        /// Metodo que genera el evento de informacion del panle lateral
        /// </summary>
        /// <param name="sender">objeto que general el evento</param>
        /// <param name="e">argumento de mause button</param>
        /// <param name="tracks">Track que fue seleccionado</param>
        /// <param name="_uid">identificador del track</param>
        private void OnTrackDetail(object sender, MouseButtonEventArgs e, Tracks tracks, string _uid)
        {
            _ea.GetEvent<EventPanel>().Publish(new DetailPanel { Track = tracks, Action = "Show", Target = "Tracks", Guid_ = _uid });
        }
        /// <summary>
        /// Metodo que limpia objetos de un tag especifico del mapa geografico
        /// </summary>
        /// <param name="tag">tag que se desea limpiar</param>
        /// <param name="cRule">bandera para la eleminacion de mediciones</param>
        /// <returns>siempre retorna true</returns>
        private bool ClearScreen(string tag, bool cRule)
        {
            var mapLayers = from r in GeoLayerMod.MyMap.Children.OfType<System.Windows.Shapes.Ellipse>()

                            select r;

            var layers = mapLayers.ToList();
            foreach (var pl in layers)
            {
                GeoLayerMod.MyMap.Children.Remove(pl);
            }


            if (cRule)
            {
                var mapLayers2 = from x in GeoLayerMod.MyMap.Children.OfType<Microsoft.Maps.MapControl.WPF.MapPolyline>()
                                 select x;

                var layers2 = mapLayers2.ToList();
                foreach (var pl in layers2)
                {
                    GeoLayerMod.MyMap.Children.Remove(pl);
                }

                var mapLayers3 = from x in GeoLayerMod.MyMap.Children.OfType<TextBlock>()
                                 select x;

                var layers3 = mapLayers3.ToList();
                foreach (var pl in layers3)
                {
                    GeoLayerMod.MyMap.Children.Remove(pl);
                }

            }

            GeoLayerMod.MyMap.UpdateLayout();

            return true;
        }

        /// <summary>
        /// Metodo ClearScreenPlots, limpia un plot especifico de la pantalla
        /// </summary>
        /// <param name="uid">identificador del plot</param>
        /// <returns>retorna true al borrar el plot</returns>
        public bool ClearScreenPlots(string uid)
        {
            var mapLayers = from r in GeoLayerMod.MyMap.Children.OfType<System.Windows.Shapes.Rectangle>()
                            where (r.Uid == uid)
                            select r;



            var layers = mapLayers.ToList();

            if (layers.Count > 0)
            {
                foreach (var pl in layers)
                {
                    GeoLayerMod.MyMap.Children.Remove(pl);
                    // GeoLayerMod.MyMap.UpdateLayout();
                }
            }

            return true;
        }

        /// <summary>
        /// Metodo que limpia objetos del mapa geografico
        /// </summary>
        /// <param name="tag">tag del objeto que se desea limpiar</param>
        /// <param name="uid">identificador unico del objeto</param>
        /// <returns>simpre retorna True</returns>
        private bool ClearScreen(string tag, string uid)
        {
            int sons = GeoLayerMod.MyMap.Children.Count;

            var mapLayers = from r in GeoLayerMod.MyMap.Children.OfType<MapPolygon>()
                            where (r.Tag == tag && r.Uid == uid)
                            select r;



            var layers = mapLayers.ToList();

            if (layers.Count > 0)
            {
                foreach (var pl in layers)
                {
                    GeoLayerMod.MyMap.Children.Remove(pl);
                    // GeoLayerMod.MyMap.UpdateLayout();
                }
            }

            return true;
        }
        /// <summary>
        /// Metodo que limpia objetos del mapa geografico
        /// </summary>
        /// <param name="tag">tag de los objetos que se desean quitar</param>
        /// <returns>siempre retorna true</returns>
        private bool ClearScreen(string tag)
        {

            var aa = GeoLayerMod.MyMap.Children;
            var mapLayers = from r in GeoLayerMod.MyMap.Children.OfType<MapPolygon>()
                            where r.Tag == tag
                            select r;

            var layers = mapLayers.ToList();
            foreach (var pl in layers)
            {
                GeoLayerMod.MyMap.Children.Remove(pl);
            }
            //GeoLayerMod.MyMap.Children.Remove(plots[0]);
            GeoLayerMod.MyMap.UpdateLayout();
            return true;
        }

        /// <summary>
        /// Metodo OnInitMap, inicializa los parametros de la propiedad GeoLayerMod
        /// </summary>
        private void OnInitMap()
        {
            GeoLayerMod.MyMap = new Map();
            GeoLayerMod.DefinedMap = false;
            GeoLayerMod.MyMap.CredentialsProvider = new ApplicationIdCredentialsProvider("uo1DQnP5LPsDQWD30rRZ~lrxjGqoZUj3EDfJKtqY_WA~AlyxblfQbNvAowODv_rFug_EZTX1fhoGfK0WY8xDyKLdiZLT7FC0YVipq10qGQE-");
            GeoLayerMod.MyMap.Center = new Location(4.15, -73.633);
            GeoLayerMod.NameStage = string.Empty;
            GeoLayerMod.MyMap.ZoomLevel = 10;
            GeoLayerMod.MyMap.Mode = new AerialMode();


            GeoLayerMod.MyMap.MouseDoubleClick += MapMouseDoubleClick;
           

            var _layers = DSconnection.DSConnection.GetLayers();

            foreach (var l in _layers)
            {
                if (l.Sigla == "Track")
                {
                    _layerTracks = l.State;
                }
                if (l.Sigla == "Plots")
                {
                    _layerPlots = l.State;
                }
                if (l.Sigla == "Devices")
                {
                    _layerDevices = l.State;
                }
            }

        }
        /// <summary>
        /// Metodo que dibuja un poligono en la capa area objetivo 
        /// </summary>
        /// <param name="locationCollection">coleccion de ubicaciones del poligon</param>
        private void LayerSurveillanceSurface(LocationCollection locationCollection)
        {
            var mapPolygon = new MapPolygon();
            mapPolygon.Locations = locationCollection;
            mapPolygon.Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            mapPolygon.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
            mapPolygon.StrokeThickness = 5;
            mapPolygon.Opacity = 0.35;
            mapPolygon.Focus();
            mapPolygon.Tag = "GeoLayer";

            GeoLayerMod.MyMap.Children.Add(mapPolygon);

            GeoLayerMod.MyMap.ZoomLevel = 16;
            var aa = GeoLayerMod.MyMap.Heading;
        }
        /// <summary>
        /// Metodo AddDevice, añade un dispositivo radar y pinta su ubicación en el area de vigilancia
        /// </summary>
        /// <param name="device">Modelo del radar</param>
        private void AddDevice(RadarDevicesModel device)
        {

            if (CollectionServicesTcp.Count < _maxDevices)
            {

                var element = CollectionServicesTcp.IndexOf(CollectionServicesTcp.Where(x => x.GuidRadar == device.GuidRadar).FirstOrDefault());


                if (element == -1)
                {
                    CollectionServicesTcp.Add(new ServiceTcp(device.IpAddress, device.Port, device.GuidRadar) { Device = device });
                    Devices.Add(device);
                    MapLayer mapLayer = new MapLayer();

                    var gis = new GisLibrary.Proj4();
                    var latitud = device.Latitud;
                    var longitud = device.Longitud;
                    // var point = gis.Convertion(latitud, longitud, GeoLayerMod.MyMap.Heading, device.Azimuth, 150, 20);
                    var point = gis.ConvertionSurface(latitud, longitud, GeoLayerMod.MyMap.Heading, device.NorthHeiding, 150, 20);

                    var cone = DrawPolygon(point, new Location() { Latitude = device.Latitud, Longitude = device.Longitud }, CollectionServicesTcp.Count);
                    cone.Uid = Convert.ToString(device.GuidRadar);

                    var icon = new PackIcon
                    {
                        Kind = PackIconKind.Video,
                        Foreground = System.Windows.Media.Brushes.Black,
                        RenderSize = new System.Windows.Size(24, 24)

                    };

                    mapLayer.Uid = Convert.ToString(device.GuidRadar);

                    var pins = new Pushpin();
                    pins.RenderSize = new Size { Height = 30, Width = 30 };
                    pins.Content = icon;
                    pins.Background = Brushes.Tomato;
                    pins.Location = new Location(device.Latitud, device.Longitud);
                    mapLayer.Visibility = (_layerDevices == true) ? Visibility.Visible : Visibility.Hidden;
                    pins.MouseDown += new MouseButtonEventHandler((s, g) => OnSelectRadar(s, g, device));
                    mapLayer.AddChild(pins, new Location(device.Latitud, device.Longitud));
                    mapLayer.Tag = "Devices";

                    GeoLayerMod.MyMap.Children.Add(mapLayer);

                    GeoLayerMod.MyMap.Children.Add(cone);

                }
            }
        }

        /// <summary>
        /// Añade un dispositivo radar a la consola
        /// </summary>
        /// <param name="device">dispositivo radar</param>
        /// <param name="reset">bandera de reset</param>
        private void AddDevice(RadarDevicesModel device, bool reset)
        {
            MapLayer mapLayer = new MapLayer();

            var gis = new GisLibrary.Proj4();
            var latitud = device.Latitud;
            var longitud = device.Longitud;
            var point = gis.ConvertionSurface(latitud, longitud, GeoLayerMod.MyMap.Heading, device.NorthHeiding, 150, 20);

            var cone = DrawPolygon(point, new Location() { Latitude = device.Latitud, Longitude = device.Longitud }, CollectionServicesTcp.Count);
            cone.Uid = Convert.ToString(device.GuidRadar);

            var icon = new PackIcon
            {
                Kind = PackIconKind.Video,
                Foreground = System.Windows.Media.Brushes.Black,
                RenderSize = new System.Windows.Size(24, 24)

            };

            mapLayer.Uid = Convert.ToString(device.GuidRadar);

            var pins = new Pushpin();
            pins.RenderSize = new Size { Height = 30, Width = 30 };
            pins.Content = icon;
            pins.Background = Brushes.Tomato;
            pins.Location = new Location(device.Latitud, device.Longitud);

            pins.Visibility = (_layerDevices == true) ? Visibility.Visible : Visibility.Hidden;
            mapLayer.Visibility = Visibility.Visible;
            pins.MouseDown += new MouseButtonEventHandler((s, g) => OnSelectRadar(s, g, device));
            mapLayer.AddChild(pins, new Location(device.Latitud, device.Longitud));
            mapLayer.Tag = "Devices";

            GeoLayerMod.MyMap.Children.Add(mapLayer);

            GeoLayerMod.MyMap.Children.Add(cone);

            var element = CollectionServicesTcp.IndexOf(CollectionServicesTcp.Where(x => x.GuidRadar == device.GuidRadar).FirstOrDefault());



        }

        /// <summary>
        /// Metodo DrawPlots, dibuja los plots en el mapa geografico
        /// </summary>
        /// <param name="plts">objeto plot</param>
        /// <param name="device">dispositivo radar</param>f
        /// <param name="b">bandera de entrada</param>
        private void DrawPlots(Plots plts, RadarDevicesModel device, bool b)
        {
            var latitud = device.Latitud;
            var longitud = device.Longitud;
            Posicion point = Proj4.ConvertionLocation(latitud, longitud, device.NorthHeiding, plts.Range, plts.Azimuth);

            var myRect = new System.Windows.Shapes.Rectangle();
            myRect.Stroke = System.Windows.Media.Brushes.Black;
            myRect.Fill = System.Windows.Media.Brushes.Yellow;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Center;
            myRect.Height = 6;
            myRect.Width = 6;
            Location location = new Location()
            {
                Latitude = point.Lat,
                Longitude = point.Lon
            };

            var toolTip = new ToolTip();
            toolTip.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            toolTip.Content = string.Format("Distancia: {0} m \nAzimuth: {1}° \nVelocidad: {2} m/s", plts.Range, Math.Round(plts.Azimuth, 2), plts.Velocity_obj);
            myRect.ToolTip = toolTip;
            //Console.WriteLine(toolTip.Content);
            //myRect.MouseLeftButtonDown += MyRect_MouseLeftButtonDown;
            myRect.MouseLeftButtonDown += (sender, e) => MyRect_MouseLeftButtonDown(sender, e, plts);
            string _uid = plts.PlotGuid + "-" + plts.RadarId;
            myRect.Tag = "Plots";
            myRect.Uid = _uid;


            Point p0 = GeoLayerMod.MyMap.LocationToViewportPoint(location);
            Point p1 = new Point(p0.X - (myRect.Width / 2), p0.Y - (myRect.Height / 2));

            Location loc = GeoLayerMod.MyMap.ViewportPointToLocation(p1);
            MapLayer.SetPosition(myRect, loc);

            GeoLayerMod.MyMap.Children.Add(myRect);

            GeoLayerMod.MyMap.UpdateLayout();
        }

        /// <summary>
        /// Metoodo que envia los datos de plos a
        /// la ventana de blancos 
        private void MyRect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e, Plots plts)
        {
            _ea.GetEvent<EventPlots>().Publish(new Plots {
                Range = plts.Range,
                Azimuth = plts.Azimuth,
                Velocity_obj = plts.Velocity_obj,
                RadarId= plts.RadarId,
                

            });
            
        }





        /// <summary>
        /// Metoodo que crea poligonos
        /// </summary>
        /// <param name="locations">lista de posiciones del poligono</param>
        /// <param name="color">Objeto de tipo brush que crea define el color del poligono</param>
        /// <returns>retorna un poligo creado con las posiciones y color definidos</returns>
        private static MapPolygon DrawPolygon(IList<Posicion> locations, Brush color)
        {
            MapPolygon mapPolygon = new MapPolygon();
            mapPolygon.Stroke = Brushes.Black;
            mapPolygon.Fill = color;
            mapPolygon.StrokeThickness = 1;
            var points = new LocationCollection();

            foreach (var kvp in locations)
            {
                points.Add(new Location() { Latitude = kvp.Lat, Longitude = kvp.Lon });
            }
            mapPolygon.Locations = points;

            return mapPolygon;
        }

        /// <summary>
        /// Metoodo que crea poligonos
        /// </summary>
        /// <param name="locations">lista de posicones del poligono</param>
        /// <param name="startlocation">pocision de inicion </param>
        /// <param name="count">entero que define el color del relleno</param>
        /// <returns>retonar el poligono creado con las caracteristicas definidas</returns>
        private MapPolygon DrawPolygon(IList<Posicion> locations, Location startlocation, int count)
        {

            MapPolygon mapPolygon = new MapPolygon();
            mapPolygon.Stroke = Brushes.Black;
            mapPolygon.Fill = Colors(count);
            mapPolygon.StrokeThickness = 1;
            mapPolygon.Opacity = 0.3;
            var points = new LocationCollection();
            points.Add(startlocation);
            foreach (var kvp in locations)
            {
                points.Add(new Location() { Latitude = kvp.Lat, Longitude = kvp.Lon });
            }
            mapPolygon.Locations = points;

            return mapPolygon;
        }

        /// <summary>
        /// Metodo que genera los evento de seleccion de los radares sobre el mapa
        /// </summary>
        /// <param name="sender">objeto que genera el evento</param>
        /// <param name="e">argumento mouse button</param>
        /// <param name="device">dispositvo radar sobre el cual se realiza la accion</param>
        private void OnSelectRadar(object sender, MouseButtonEventArgs e, RadarDevicesModel device)
        {

            var collectionDevice = DSconnection.DSConnection.GetDevicesList();
            RadarDevicesModel radar = collectionDevice.Where(x => x.GuidRadar == device.GuidRadar).First();

            _ea.GetEvent<EventPanel>().Publish(new DetailPanel { Device = radar, Action = "Show", Target = "Device" });
        }

        /// <summary>
        /// Metodo que configura el área objetivo
        /// </summary>
        private void SetArea()
        {
            var count = DSconnection.DSConnection.CountDevices();

            ClearScreen("GeoLayer");
            double mapHigh;
            double mapWidth;
            LocationCollection location = new LocationCollection(); ;

            location.Add(new Location(GeoLayerMod.TargetAreaModel_.LatitudP1.Value, GeoLayerMod.TargetAreaModel_.LongitudP1.Value));
            location.Add(new Location(GeoLayerMod.TargetAreaModel_.LatitudP1.Value, GeoLayerMod.TargetAreaModel_.LongitudP2.Value));
            location.Add(new Location(GeoLayerMod.TargetAreaModel_.LatitudP2.Value, GeoLayerMod.TargetAreaModel_.LongitudP2.Value));
            location.Add(new Location(GeoLayerMod.TargetAreaModel_.LatitudP2.Value, GeoLayerMod.TargetAreaModel_.LongitudP1.Value));
            mapWidth = Math.Abs((GeoLayerMod.TargetAreaModel_.LatitudP1.Value) - (GeoLayerMod.TargetAreaModel_.LatitudP2.Value));
            mapHigh = Math.Abs((GeoLayerMod.TargetAreaModel_.LongitudP1.Value) - (GeoLayerMod.TargetAreaModel_.LongitudP2.Value));

            GeoLayerMod.ZoomLevel = 16;
            var X = (GeoLayerMod.TargetAreaModel_.LatitudP1.Value + GeoLayerMod.TargetAreaModel_.LatitudP2.Value) / 2;
            var Y = (GeoLayerMod.TargetAreaModel_.LongitudP1.Value + GeoLayerMod.TargetAreaModel_.LongitudP2.Value) / 2;

            GeoLayerMod.MyMap.Center = new Location(X, Y);
            LayerSurveillanceSurface(location);
            GeoLayerMod.DefinedMap = true;

            GeoLayerMod.MyMap.Heading = 0;
            GeoLayerMod.MyMap.MouseWheel += ZoomLimits;


            GeoLayerMod.MyMap.UpdateLayout();
            //  GeoLayerMod.MyMap.ViewChangeOnFrame += BoundariesView; 
            boundingRectangle = GeoLayerMod.MyMap.BoundingRectangle;
            GeoLayerMod.MyMap.ViewChangeOnFrame += BoundariesView2;
        }

        /// <summary>
        /// Evento que define los limites de desplazamiento una vez se ha definido un area objetivo
        /// </summary>
        /// <param name="sender">objeto que activa el eventoi</param>
        /// <param name="e">MapEventArgs</param>
        private void BoundariesView2(object sender, MapEventArgs e)
        {

            var width = (Math.Abs((GeoLayerMod.TargetAreaModel_.LatitudP1.Value - GeoLayerMod.TargetAreaModel_.LatitudP2.Value))) / 2;
            var height = (Math.Abs((GeoLayerMod.TargetAreaModel_.LongitudP1.Value - GeoLayerMod.TargetAreaModel_.LongitudP2.Value))) / 2;


            var maxLat = width + GeoLayerMod.TargetAreaModel_.LatitudP1.Value;
            var minLat = GeoLayerMod.TargetAreaModel_.LatitudP2.Value - width;
            var maxLong = GeoLayerMod.TargetAreaModel_.LongitudP2.Value + height;
            var minLong = GeoLayerMod.TargetAreaModel_.LongitudP1.Value - height;

            var map = sender as Map;
            var zoomLevel = GeoLayerMod.MyMap.ZoomLevel;
            var center = new Location() { Latitude = maxLat, Longitude = GeoLayerMod.MyMap.Center.Longitude };

            if (map.TargetCenter.Latitude > maxLat)
            {
                center.Latitude = maxLat;
                center.Longitude = GeoLayerMod.MyMap.Center.Longitude;
                GeoLayerMod.MyMap.SetView(center, zoomLevel);

            }
            if (map.TargetCenter.Latitude < minLat)
            {
                center.Latitude = minLat;
                center.Longitude = GeoLayerMod.MyMap.Center.Longitude;
                GeoLayerMod.MyMap.SetView(center, zoomLevel);

            }

            if (map.TargetCenter.Longitude < minLong)
            {
                center.Latitude = GeoLayerMod.MyMap.Center.Latitude;
                center.Longitude = minLong;
                GeoLayerMod.MyMap.SetView(center, zoomLevel);

            }
            if (map.TargetCenter.Longitude > maxLong)
            {
                center.Latitude = GeoLayerMod.MyMap.Center.Latitude;
                center.Longitude = maxLong;
                GeoLayerMod.MyMap.SetView(center, zoomLevel);
            }

            GeoLayerMod.MyMap.UpdateLayout();
        }


        /// <summary>
        /// Metodo que cambia el estilo del mouse
        /// </summary>
        /// <param name="sender">objeto que genera el evento</param>
        /// <param name="e">argumento mouse button</param>
        private void MapMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            /*var latitud = device.Latitud;
            var longitud = device.Longitud;
            Posicion point = Proj4.ConvertionLocation(latitud, longitud, device.NorthHeiding, plts.Range, plts.Azimuth);

            var myRect = new System.Windows.Shapes.Rectangle();
            myRect.Stroke = System.Windows.Media.Brushes.Black;
            myRect.Fill = System.Windows.Media.Brushes.Yellow;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Center;
            myRect.Height = 6;
            myRect.Width = 6;
            Location location = new Location()
            {
                Latitude = point.Lat,
                Longitude = point.Lon
            };

            var toolTip = new ToolTip();
            toolTip.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
            toolTip.Content = string.Format("Distancia: {0} m \nAzimuth: {1}° \nVelocidad: {2} Km/h", plts.Range, Math.Round(plts.Azimuth, 2), plts.Velocity_obj);
            myRect.ToolTip = toolTip;
            Console.WriteLine(toolTip.Content);*/


        }

        /// <summary>
        /// Metodo que establece limites al ZoomIn & ZoomOut una vez se ha definido el área objetivos
        /// </summary>
        /// <param name="sender">obkjeto el evento</param>
        /// <param name="e">argumento Mouse Wheel</param>
        private void ZoomLimits(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;

            if (e.Delta > 0 && GeoLayerMod.MyMap.ZoomLevel <= 20)
            {
                GeoLayerMod.MyMap.ZoomLevel += 0.2;
            }
            if (e.Delta < 0 && GeoLayerMod.MyMap.ZoomLevel >= 16)
            {
                GeoLayerMod.MyMap.ZoomLevel -= 0.2;
            }
            if (GeoLayerMod.MyMap.ZoomLevel >= 19)
            {
                GeoLayerMod.MyMap.ZoomLevel = 19;
            }
            if (GeoLayerMod.MyMap.ZoomLevel < 16)
            {
                GeoLayerMod.MyMap.ZoomLevel = 16;
            }
        }

        /// <summary>
        /// Metodo SolidColorBrush, difine el color de los conos de los dispositivos radar
        /// </summary>
        /// <param name="i">indice del radar</param>
        /// <returns>retorna color definido segun su indice</returns>
        public static SolidColorBrush Colors(int i)
        {
            switch (i)
            {
                case 1: return Brushes.AliceBlue;
                case 2: return Brushes.AntiqueWhite;
                case 3: return Brushes.BurlyWood;
                case 4: return Brushes.LightGreen;
                case 5: return Brushes.LightGoldenrodYellow;
                case 6: return Brushes.LightPink;
                case 7: return Brushes.LightSeaGreen;
                case 8: return Brushes.Magenta;
                case 9: return Brushes.Maroon;
                case 10: return Brushes.Tan;

                default: return Brushes.Beige;
            }

        }
        #endregion
    }
}
