using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using PRORAM.Models.TPC;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using PRORAM.Models;
using System.IO;

namespace PRORAM.ServicesTcp
{

    // State object for receiving data from remote device.  
    public class StateObject
    {
        // Client socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 256;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public StringBuilder sb = new StringBuilder();
    }
    /// <summary>
    /// Clase ServiceTcp contiene los metodos y rutinas para la conexión entre la consola y el dispositivo radar
    /// </summary>
    public class ServiceTcp : BindableBase
    {
        #region private 
        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);
        private static ManualResetEvent disconnectDone =
            new ManualResetEvent(false);

        // The response from the remote device.  
        private static String response = String.Empty;

        private ObservableCollection<Plots> _plotsFromRadar;
        private RadarDevicesModel _device;
        private Task _updateTask;
        private Task _connectionTask;
        private ObservableCollection<Tracks> _tracksFromRadar;
        private IPAddress _ipAddress;
        private static Socket client;
        private int _Port;

        #endregion
        public System.Windows.Forms.Timer _timerGetStatus;
        /// <summary>
        /// Propiedad _isRunning, dice el estatus si esta radiando 
        /// </summary>
        public bool _isRunning;
        /// <summary>
        /// Propiedad ResultTrans, array de byte's
        /// </summary>
        public static byte[] ResultTrans = new byte[256];
        /// <summary>
        /// Delegado EventPltos encargado de los eventos sobre los plots
        /// </summary>
        /// <param name="plt">objeto con las propiedades del plot</param>
        /// <param name="action">string con la accion realizada</param>
        /// <param name="radarDevice">dispositivo radar que genero el plot</param>
        public delegate void EventPltos(Plots plt, string action, RadarDevicesModel radarDevice);
        /// <summary>
        /// Evento ActionsPlot del tipo EventPltos
        /// </summary>
        public event EventPltos ActionsPlot;
        /// <summary>
        /// Delegado EventTracks encargado de los eventos sobre los tracks
        /// </summary>
        /// <param name="trc">objeto con las propiedades del track</param>
        /// <param name="action">string con la accion realizada</param>
        /// <param name="radarDevice">dispositivo radar que genero el track</param>
        public delegate void EventTracks(Tracks trc, string action, RadarDevicesModel radarDevice);
        /// <summary>
        /// Evento ActionsPlot del tipo EventTracks
        /// </summary>
        public event EventTracks ActionsTrack;
        /// <summary>
        /// Delegado EventDisconnection encargado del evento de desconexión
        /// </summary>
        /// <param name="IsDisconnected">bandera de desconexión</param>
        /// <param name="Device">Dispositivo radar que genera el evento</param>
        public delegate void EventDisconnection(bool IsDisconnected, RadarDevicesModel Device);
        /// <summary>
        /// Evento RadarDisconnection del tipo EventDisconnection
        /// </summary>
        public event EventDisconnection RadarDisconnection;
        /// <summary>
        /// Propiedad GuidRadar, almacena la el Gui del radar
        /// </summary>
        public Guid GuidRadar;
        /// <summary>
        /// Propiedad IsConnected, almacena el estado de la conexión
        /// </summary>
        public bool IsConnected;
        /// <summary>
        /// Propiedad Status, define el estado actual de la conexión
        /// </summary>
        public string Status;
        /// <summary>
        /// Propiedad Device,modelo dispositivo radar
        /// </summary>
        public RadarDevicesModel Device
        {
            get { return _device; }
            set { SetProperty(ref _device, value); }
        }
        /// <summary>
        /// Propiedad PlotsFromRadar, Colección de plots
        /// </summary>
        public ObservableCollection<Plots> PlotsFromRadar
        {
            get { return _plotsFromRadar; }
            set { SetProperty(ref _plotsFromRadar, value); }
        }
        /// <summary>
        /// Propiedad TracksFromRadar, Colección de tracks
        /// </summary>
        public ObservableCollection<Tracks> TracksFromRadar
        {
            get { return _tracksFromRadar; }
            set { SetProperty(ref _tracksFromRadar, value); }
        }


        /// <summary>
        /// Constructor de la clase ServiceTcp incia la instancia del objeto con los parametros iniciales
        /// </summary>
        /// <param name="ip">Ip del radar al que se quiere conectar</param>
        /// <param name="port">Puerto del radar al que se quiere conectar</param>
        /// <param name="guidRadar">identificador unico del radar</param>
        public ServiceTcp(string ip, int port, Guid guidRadar)
        {
            _ipAddress = IPAddress.Parse(ip);
            _Port = port;
            GuidRadar = guidRadar;
            IsConnected = false;
            PlotsFromRadar = new ObservableCollection<Plots>();
            TracksFromRadar = new ObservableCollection<Tracks>();
            Device = new RadarDevicesModel();
            Status = "Disconnect";

            _timerGetStatus = new System.Windows.Forms.Timer();
            _timerGetStatus.Tick += new System.EventHandler(GetStatusConnection);
            _timerGetStatus.Interval = 5000;
        }
        /// <summary>
        /// Evento GetStatusConnection, encargado de consultar el estado de la conexión
        /// </summary>
        /// <param name="sender">objeto que hace el llamado</param>
        /// <param name="e">EventArgs</param>
        private void GetStatusConnection(object sender, EventArgs e)
         {

            var messagesGenerator = new MessagesGenerator(1);
            var messageToSend = messagesGenerator.GetSettingTime(85);
            var _resultCheck = SendAndReceive(messageToSend, 85);


        }


        /// <summary>
        /// Metodo StartClient, inicia la conexión al dispositivo radar
        /// </summary>
        public void StartClient()
        {

            // Connect to a remote device.  
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(this._ipAddress, this._Port);

                // Create a TCP/IP socket.  
                client = new Socket(_ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                client.ReceiveBufferSize = 256;
                client.SendBufferSize = 256;
                client.ReceiveTimeout = 100;
                client.NoDelay = true;
                connectDone.Reset();
                // Connect to the remote endpoint.  
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);

                connectDone.WaitOne(100, true);
                if (client.Connected)
                {
                    Status = "Connected";
                    _isRunning = false;
                    IsConnected = true;
                }
                else
                {
                    Status = "Disconnect";
                    _isRunning = false;
                    IsConnected = false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Metodo ConnectCallback, termina de relizar la conexión asincrona
        /// </summary>
        /// <param name="ar">socket</param>
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Metodo IsSocketConnected, revisa el estado de la conexión
        /// </summary>
        /// <returns></returns>
        private bool IsSocketConnected()
        {
            try
            {
                bool part1 = client.Poll(100, SelectMode.SelectRead);
                bool part2 = (client.Available == 0);
                if (part1 && part2)
                {
                    RadarDisconnection?.Invoke(true, this.Device);

                    Console.WriteLine("El radar ha sido desconectado");
                    IsConnected = false;
                    _isRunning = false;
                    return true;
                    // throw new Exception();
                }
                else
                {
                    return false;
                }

            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("IsSocketConnected " + e.Message);
                return false;
            }
        }
        /// <summary>
        /// Metodo ClearPlots, remueve los plots que llevan mas de 5 segundo en ser agregados
        /// </summary>
        private void ClearPlots()
        {
            if (PlotsFromRadar.Count > 0)
            {
                var plots = PlotsFromRadar.ToArray();

                foreach (var plt in plots)
                {
                    try
                    {
                        if (plt != null)
                        {
                            var timeStamp = plt.TimeStamp.AddSeconds(5);
                            var now = DateTime.Now;

                            if (timeStamp.CompareTo(DateTime.Now) < 0)
                            {
                                PlotsFromRadar.Remove(plt);
                                ActionsPlot?.Invoke(plt, "OnDeletePlot", Device);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(ex, true);
                        Console.WriteLine("plt nulo archivo " + trace.GetFrame(0).GetFileName() + " linea " + trace.GetFrame(0).GetFileLineNumber());
                    }
                }
            }
        }
        /// <summary>
        /// Metodo ClearTracks, remueve los tracks que llevan mas de 5 segundos en ser agregados
        /// </summary>
        private void ClearTracks()
        {
            if (TracksFromRadar.Count > 0)
            {
                var tracks = TracksFromRadar.ToArray();

                foreach (var trc in tracks)
                {
                    var timeStamp = trc.TimeStamp.AddSeconds(5);
                    var now = DateTime.Now;

                    if (timeStamp.CompareTo(DateTime.Now) < 0)
                    {
                        TracksFromRadar.Remove(trc);
                        ActionsTrack?.Invoke(trc, "OnDeleteTrack", Device);
                    }
                }
            }
        }
        /// <summary>
        /// Metodo StopRunning, detiene la recepcion de blancos del radar
        /// </summary>
        public void StopRunning()
        {
            if (IsConnected)
            {
                this._timerGetStatus.Enabled = true;
                this._isRunning = false;
            }

        }


        /// <summary>
        /// Metodo StartRunning, habilita la recepcion de mensajes de blancos 
        /// </summary>
        /// <param name="status"></param>
        public void StartRunning()
        {
            if (IsConnected)
            {
                this._timerGetStatus.Enabled = false;
                this._isRunning = true;
            }
        }
        /// <summary>
        /// Metodo UpdateConnection,revisa los mensajes recividos por el dispositivo radar
        /// </summary>
        /// <returns>tarea asyncrona retorna true cuando finaliza la lectura de mensajes</returns>
        public async Task<bool> UpdateConnection()
        {
            //  Console.WriteLine("is Running {0}, is Connected {1}", _isRunning, IsConnected);
            while (IsConnected)
            {
                ClearPlots();
                ClearTracks();
                receiveDone.Reset();
                Receive(client);
                receiveDone.WaitOne(100, true);

                await App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (ResultTrans[0] == ListMessages.headerMessages["Rep_Plots"] && _isRunning == true)
                    {
                        byte[] data = ResultTrans;
                        int sizeData = data[3];
                        int endFlag = 4 + sizeData;
                        byte[] time = ArraySub.SubArray(data, endFlag - 4, 4);
                        RepPlots(data, time, endFlag);
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (ResultTrans[0] == ListMessages.headerMessages["Rep_Track"] && _isRunning == true)
                    {
                        byte[] data = ResultTrans;
                        int sizeData = data[3];
                        int endFlag = 4 + sizeData;
                        byte[] time = ArraySub.SubArray(data, endFlag - 4, 4);
                        RepTracks(data, time, endFlag);
                    }
                }));
                IsSocketConnected();
            }
            return true;
        }

        /// <summary>
        /// Metodo DisconnectedRadar, termina la conexión con el dispositivo radar y la consola.
        /// </summary>
        public void DisconnectedRadar()
        {
            try
            {
                disconnectDone.Reset();
                client.Shutdown(SocketShutdown.Both);
                client.BeginDisconnect(true, new AsyncCallback(DisconnectCallback), client);
                disconnectDone.WaitOne();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            if (client.Connected)
            {
                Status = "Connected";
                Console.WriteLine("We're still connected");
            }
            else
            {
                var plts = PlotsFromRadar.ToArray();
                foreach (Plots p in plts)
                {
                    PlotsFromRadar.Remove(p);
                    ActionsPlot?.Invoke(p, "OnDeletePlot", Device);
                }

                var trcs = TracksFromRadar.ToArray();
                foreach (Tracks t in trcs)
                {
                    TracksFromRadar.Remove(t);
                    ActionsTrack?.Invoke(t, "OnDeleteTrack", Device);
                }

                RadarDisconnection?.Invoke(true, this.Device);
                _isRunning = false;
                IsConnected = false;
                this._timerGetStatus.Enabled = false;
                Status = "Disconect";
                Console.WriteLine("We're disconnected");

            }
        }

        /// <summary>
        /// Metodo que termine la desconeccion asyncrona
        /// </summary>
        /// <param name="ar">objeto resultado del socket asyncrono</param>
        private void DisconnectCallback(IAsyncResult ar)
        {
            // Complete the disconnect request.
            Socket client = (Socket)ar.AsyncState;
            client.EndDisconnect(ar);

            // Signal that the disconnect is complete.
            _timerGetStatus.Enabled = false;
            disconnectDone.Set();
        }

        /// <summary>
        /// Metodo Receive, revibe los mensajes provenientes del dispositivo radar
        /// </summary>
        /// <param name="client">objeto del Socket que contiene los metodos de el envio y recepcion de mensajes tcp</param>
        private void Receive(Socket client)
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                     new AsyncCallback(ReceiveCallback), state);

            }
            catch (Exception e)
            {
                DisconnectedRadar();
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Metodo ReceiveCallback, llamada async para recivir la totalidad del mensaje
        /// </summary>
        /// <param name="ar"></param>
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    //client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None,
                    //    new AsyncCallback(ReceiveCallback), state);
                }

                int bytesRemain = state.workSocket.Available;
                if (bytesRemain > 0)                                      // Add to fix
                {                                                       // Add to fix                                                                        //  Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, SocketFlags.None,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.sb.Length >= 1)
                    {
                        ResultTrans = state.buffer;
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Metodo SendAndReceive, envia un mensaje al radar y espera la respuesta del radar
        /// </summary>
        /// <param name="data">byte que se enviaran al radar</param>
        /// <param name="idMessage">id</param>
        /// <returns>retorna la respuesta del estado de radiación</returns>
        public bool SendAndReceive(byte[] data, byte idResponce)
        {
            bool control = true;
            int count = 0;
            var _resultTrans = new byte[256];
            int resultR = 0;
            if (IsConnected == true)
            {

                sendDone.Reset();
                // Begin sending the data to the remote device.  
                Send(client, data);
                sendDone.WaitOne(100, true);

                // Receive the response from the remote device.  
                //Receive(client);    
                receiveDone.Reset();

                while (control)
                {

                    try
                    {
                        if (IsConnected == true)
                        {
                            // Create the state object.  
                            StateObject state = new StateObject();
                            state.workSocket = client;

                            // Begin receiving the data from the remote device.  
                            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                                 new AsyncCallback(ReceiveCallback), state);
                            receiveDone.WaitOne();
                            _resultTrans = state.buffer;
                            count += 1;
                            if (count == 50 || _resultTrans[0] == idResponce)
                            {
                                resultR = state.buffer[0];

                                control = false;
                                return (resultR == idResponce) ? true : false;
                            }
                        }
                        else
                        {
                            control = false;
                        }
                    }

                    catch (Exception e)
                    {
                        DisconnectedRadar();
                        Console.WriteLine(e.ToString());
                    }
                }
                // receiveDone.WaitOne();
                Console.WriteLine("El id de la respuesta es " + _resultTrans[0]);
                return (resultR == idResponce) ? true : false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Metodo SendAndReceive, envia un mensaje al radar y espera la respuesta del radar
        /// </summary>
        /// <param name="data">byte que se enviaran al radar</param>
        /// <returns></returns>
        public byte[] SendAndReceive(byte[] data)
        {
            if (IsConnected == true)
            {
                byte[] _resultTrans = new byte[256];
                sendDone.Reset();
                Send(client, data);
                sendDone.WaitOne();

                // Receive the response from the remote device.  
                //Receive(client);

                try
                {
                    receiveDone.Reset();
                    // Create the state object.  
                    StateObject state = new StateObject();
                    state.workSocket = client;

                    // Begin receiving the data from the remote device.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                         new AsyncCallback(ReceiveCallback), state);
                    receiveDone.WaitOne();
                    _resultTrans = state.buffer;
                    if (_resultTrans[0] == 0x00)
                    {
                        int b = 0;
                    }
                    return _resultTrans;
                }
                catch (Exception e)
                {
                    DisconnectedRadar();
                    Console.WriteLine(e.ToString());
                }

                return _resultTrans;
            }
            else
            {
                return new byte[256];
            }
        }
        /// <summary>
        /// Metodo Send, envia los mensajes de la consola al dispositivo radar
        /// </summary>
        /// <param name="client">objeto del Socket que contiene los metodos de el envio y recepcion de mensajes tcp</param>
        /// <param name="data">byte [] mensaje que se enviara al dispositivo radar</param>
        private void Send(Socket client, byte[] data)
        {
            try
            {
                // Begin sending the data to the remote device.  
                client.BeginSend(data, 0, data.Length, 0,
                    new AsyncCallback(SendCallback), client);
            }
            catch (Exception e)
            {
                DisconnectedRadar();
                Console.WriteLine(e.ToString());
            }
        }
        /// <summary>
        /// Metodo publico Send, envia los mensajes de la consola al dispositivo radar
        /// </summary>
        /// <param name="client">objeto del Socket que contiene los metodos de el envio y recepcion de mensajes tcp</param>
        /// <param name="data">byte [] mensaje que se enviara al dispositivo radar</param>
        public void sendData(byte[] data)
        {
            sendDone.Reset();
            Send(client, data);
            sendDone.WaitOne();
        }
        /// <summary>
        /// Metodo RepPlots, hace split del  mensaje y lo distribuye en plots
        /// </summary>
        /// <param name="buffer">array de byte con la informacion del mensaje</param>
        /// <param name="timeStamp">array de byte con la inforamcion de la hora de creación del plot</param>
        /// <param name="endflag">posicion con la finalizaciopn del mensaje</param>
        private void RepPlots(byte[] buffer, byte[] timeStamp, int endflag)
        {
            byte[] plots = ArraySub.SubArray(buffer, 4, endflag - 8);
            Console.WriteLine("Reporte de plots, total de plots {0}", plots.Length);
            int idRadar = buffer[1];
            var dateTimeVar = Utils.ConvertByteToTime(timeStamp);
            if (plots.Length % 6 == 0)
            {
                for (int i = 0; i < plots.Length; i = i + 6)
                {
                    var _guidPlot = Guid.NewGuid();
                    PlotsFromRadar.Add(new Plots
                    {
                        TimeStamp = dateTimeVar,
                        Range = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(plots, i, 2)).Reverse().ToArray(), 0)) / 100,
                        Azimuth = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(plots, i + 2, 2)).Reverse().ToArray(), 0)) / 10,
                        Power = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(plots, i + 4, 2)).Reverse().ToArray(), 0)),
                        Velocity_obj = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(plots, i + 3, 2)).Reverse().ToArray(), 0)),
                        RadarId = idRadar,
                        PlotGuid = _guidPlot
                    });

                    ActionsPlot?.Invoke(new Plots
                    {
                        TimeStamp = dateTimeVar,
                        Range = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(plots, i, 2)).Reverse().ToArray(), 0)) / 100,
                        Azimuth = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(plots, i + 2, 2)).Reverse().ToArray(), 0)) / 10,
                        Power = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(plots, i + 4, 2)).Reverse().ToArray(), 0)),
                        //Velocity_obj = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(plots, i + 2, 2)).Reverse().ToArray(), 0)) / 10,
                        RadarId = idRadar,
                        PlotGuid = _guidPlot,
                    }, "OnAddPlot", Device);

                }
            }
            else
            {
                Console.WriteLine("ServiceTcp tamaño de datos incorrecto");
            }
            ResultTrans = new byte[256];
        }

        /// <summary>
        /// Metodo RepTracks, hace split del  mensaje y lo distribuye en tracks
        /// </summary>
        /// <param name="buffer">array de byte con la informacion del mensaje</param>
        /// <param name="timeStamp">array de byte con la inforamcion de la hora de creación del plot</param>
        /// <param name="endflag">posicion con la finalizaciopn del mensaje</param>
        private void RepTracks(byte[] buffer, byte[] timeStamp, int endflag)
        {
            byte[] tracks = ArraySub.SubArray(buffer, 4, endflag - 8);
            //Console.WriteLine("Reporte de tracks, total de tracks {0}", tracks.Length);
            int idRadar = buffer[1];
            var dateTimeVar = Utils.ConvertByteToTime(timeStamp);
            if (tracks.Length % 6 == 0)
            {
                for (int i = 0; i < tracks.Length; i = i + 6)
                {
                    var _guidTrack = Guid.NewGuid();
                    TracksFromRadar.Add(new Tracks
                    {
                        TimeStamp = dateTimeVar,
                        Id = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i, 2)).Reverse().ToArray(), 0)),
                        Azimuth = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 2, 2)).Reverse().ToArray(), 0)) / 10,
                        PosX = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 4, 2)).Reverse().ToArray(), 0)) / 100,
                        PosY = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 6, 2)).Reverse().ToArray(), 0)) / 100,
                        VelX = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 8, 2)).Reverse().ToArray(), 0)) / 1000,
                        VelY = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 10, 2)).Reverse().ToArray(), 0)) / 1000,
                        Type = (short)(BitConverter.ToUInt16(ArraySub.SubArray(tracks, i + 10, 2).Reverse().ToArray(), 0) / 1000),
                        RadarOrigin = idRadar,
                        // TODO: revisar los atributos velocidad, distancia al radar y tiempo de vida: no están en la ICD
                    });

                    ActionsTrack?.Invoke(new Tracks
                    {
                        TimeStamp = dateTimeVar,
                        Id = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i, 2)).Reverse().ToArray(), 0)),
                        Azimuth = Convert.ToDouble(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 2, 2)).Reverse().ToArray(), 0)) / 10,
                        PosX = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 4, 2)).Reverse().ToArray(), 0)) / 100,
                        PosY = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 6, 2)).Reverse().ToArray(), 0)) / 100,
                        VelX = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 8, 2)).Reverse().ToArray(), 0)) / 1000,
                        VelY = Convert.ToInt32(BitConverter.ToUInt16((ArraySub.SubArray(tracks, i + 10, 2)).Reverse().ToArray(), 0)) / 1000,
                        Type = (short)(BitConverter.ToUInt16(ArraySub.SubArray(tracks, i + 10, 2).Reverse().ToArray(), 0) / 1000),
                        RadarOrigin = idRadar,
                    }, "OnAddTrack", Device);

                }
            }
            ResultTrans = new byte[256];
        }

        /// <summary>
        /// Metodo SendCallback, metodo invocado al finalizar el envio de datos al dispositivo radar
        /// </summary>
        /// <param name="ar"></param>
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                //   DisconnectedRadar();
                Console.WriteLine(e.ToString());
            }
        }

    }
    /// <summary>
    /// Clase static ArraySub
    /// </summary>
    public static class ArraySub
    {
        /// <summary>
        /// recorta una porcion de un array byte
        /// </summary>
        /// <typeparam name="T">parametro de tipo subarray "omitir"</typeparam>
        /// <param name="data">array con la informacion</param>
        /// <param name="index">incio donde se recortara la porcion</param>
        /// <param name="length">tamaño total que se recortara</param>
        /// <returns>retorna una porcion del array byte</returns>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            try
            {
                T[] result = new T[length];
                Array.Copy(data, index, result, 0, length);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ServiceTcp linea 804" + ex.Message);
            }
            return null;
        }
    }
    /// <summary>
    /// Clase Plots, contiene la estructura de los plots
    /// </summary>
    public class Plots
    {
        public int LifeTime { get; set; }
        public int RadarId { get; set; }
        public Guid PlotGuid { get; set; }
        public double Range { get; set; }
        public double Azimuth { get; set; }
        public int Power { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Velocity_obj { get; set; }
        

    }

    /// <summary>
    /// Clase Tracks, contiene la estructura de los tracks
    /// </summary>
    public class Tracks
    {
        public double Id { get; set; }
        public double Azimuth { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double VelX { get; set; }
        public double VelY { get; set; }
        public Int16 Type { get; set; }
        public DateTime TimeStamp { get; set; }
        public int LifeTime { get; set; }
        public int RadarOrigin { get; set; }
        public double Velocity { get; set; }
        public double DistanceToRadar { get; set; }
    }
}
