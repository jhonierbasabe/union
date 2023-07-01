using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryPRORAM
{
    public class LibPRORAM
    {
        private string _ip { get; set; }
        private string _radarName { get; set; }
        private int _port { get; set; }
        private ServiceTcp _serviceTcp { get; set; }
        private int _channel { get; set; }
        private int _txPower { get; set; }
        private static byte IdRadar { get; set; }
        private Task UpdateConnection { get; set; }
        private static bool SendPlotsEvent { get; set; }

        public string Status { get; set; }


        /// <summary>
        /// Delegado EventPltos encargado de los eventos sobre los plots
        /// </summary>
        /// <param name="plt">objeto con las propiedades del plot</param>
        /// <param name="action">string con la accion realizada</param>
        /// <param name="radarName">nombre del dispositivo radar que genero el plot</param>
        public delegate void EventPltos(Plots plt, string action, string radarName);
        /// <summary>
        /// Evento ActionsPlot del tipo EventPltos
        /// </summary>
        public event EventPltos PlotsReceivedHandler;


        /// <summary>
        /// Construcctor po defecto de la clase
        /// </summary>
        /// <param name="ip">cadena de texto con la ip del dispositivo radar</param>
        /// <param name="port">entero con el puerto de dispositivo radar</param>
        /// <param name="radarName">cadena de texto con el nombre del dispositivo radar</param>
        /// <param name="channel">entero con el canal de transmision dispositivo radar (1 o 2)</param>
        /// <param name="txpower">entero con la potencia del dispositivo radar (0-100)</param>
        public LibPRORAM(string ip, int port, string radarName, int channel, int txpower)
        {
            this._ip = ip;
            this._port = port;
            this._radarName = radarName;
            this._txPower = txpower;
            this._channel = channel;
            Status = "Disconnect";
            SendPlotsEvent = true;
            _serviceTcp = new ServiceTcp(this._ip, this._port, this._radarName);
            Console.WriteLine("Si es acá");

        }
        /// <summary>
        /// Metodo Conectar_Is3, establece la conexión con el dispositivo radar y configura los mensajes iniciales
        /// </summary>
        /// <returns>retorna una bandera booleana con el resultado de la operación</returns>
        public bool Conectar_Is3()
        {
            return Connection();
        }
        /// <summary>
        /// Metodo Desconectar_Is3, desconecta el dispositivo radar 
        /// </summary>
        /// <returns></returns>
        public bool Desconectar_Is3()
        {
            return Disconnection();
        }
        /// <summary>
        /// Metodo Arm_Is3, enciende la radiación del dispositivo radar
        /// </summary>
        public void Arm_Is3()
        {
            var messagesGenerator = new MessagesGenerator(1);
            var messageToSend = messagesGenerator.GenerateMessageEncender(1, IdRadar);

            var result = this._serviceTcp.SendAndReceive(messageToSend);

            ///this._serviceTcp.StartRunning();
            UpdateConnection = Task.Factory.StartNew(() => this._serviceTcp.UpdateConnection());

        }



        /// <summary>
        /// Metodo Disarm_Is3, apaga la radiación del dispositivo radar
        /// </summary>
        public void Disarm_Is3()
        {
            SendPlotsEvent = false;
            this._serviceTcp._isDisarm = true;
            UpdateConnection.Wait();
            this._serviceTcp._isDisarm = false;
            var messagesGenerator = new MessagesGenerator(1);
            var messageToSend = messagesGenerator.GenerateMessageEncender(0, IdRadar);
            var result = this._serviceTcp.SendAndReceive(messageToSend);


        }
        /// <summary>
        /// Metodo SendPlots_Is3, activa la transmición de plots
        /// </summary>
        public void SendPlots_Is3()
        {
            SendPlotsEvent = true;
            this._serviceTcp.PlotsFromRadar.CollectionChanged += SubrcibreEvents;
            this._serviceTcp.StartRunning();

        }
        /// <summary>
        /// Metodo StopPlots_Is3q, detiene la transmición de plots
        /// </summary>
        public void StopPlots_Is3()
        {
            SendPlotsEvent = false;
            this._serviceTcp.PlotsFromRadar.CollectionChanged -= SubrcibreEvents;
            this._serviceTcp.StopRunning();

        }
        /// <summary>
        /// Metodo SubrcibreEvents, evento que lanza cuando se ha añadido un plot
        /// </summary>
        /// <param name="sender">objeto que activa el evento</param>
        /// <param name="e">parametros del evento</param>
        private void SubrcibreEvents(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (this._serviceTcp.PlotsFromRadar.Count > 0 && SendPlotsEvent == true)
                {
                    var plt = this._serviceTcp.PlotsFromRadar.Last();
                    PlotsReceivedHandler?.Invoke(new Plots
                    {
                        Azimuth = plt.Azimuth,
                        Power = plt.Power,
                        RadarId = plt.RadarId,
                        Range = plt.Range,
                        TimeStamp = plt.TimeStamp
                    }, "OnAddPlot", this._radarName);

                }
            }
        }
        /// <summary>
        /// Metodo Disconnection, desconecta el dispositivo radar 
        /// </summary>
        /// <returns>una bandera booleana que dice el resultado de la operación</returns>
        private bool Disconnection()
        {
            if(UpdateConnection != null && UpdateConnection.Status == TaskStatus.Running)
            {
                Disarm_Is3();
            }
            this._serviceTcp.DisconnectedRadar();

            if (this._serviceTcp.IsConnected)
            {
                Status = this._serviceTcp.Status;
                return false;                
            }
            else
            {
                Status = this._serviceTcp.Status;
                return true;
            }
        }
        /// <summary>
        /// Metodo Connection, establece la conexion con el dispositivo radar, configigura los mensajes de RadarID, setTime, setTxPOWER, SetTXChannel
        /// </summary>
        /// <returns>retorna una bandera booleana con el resultado de la transación</returns>
        private bool Connection()
        {
            Status = this._serviceTcp.Status;

            Console.WriteLine(Status);
            if (Status == "Disconnect")
            {
                bool step = false;
                byte idRadar = 0;
                byte[] ResultGetIdRadar = new byte[256];
                bool isRunning = false;
                byte[] result = new byte[256];
                MessagesGenerator messagesGenerator = new MessagesGenerator(1);
                byte[] messageToSend = messagesGenerator.GenerateMessageIdRadar(1);
                string erroCode = "";

                if (this._serviceTcp.IsConnected == false)
                {
                    this._serviceTcp.StartClient();
                }
                if (this._serviceTcp.IsConnected == false)
                {
                    step = false;
                }



                //if (UpdateConnection != null && UpdateConnection.Status == TaskStatus.Running)
                //{
                //    Disarm_Is3();
                //}

                Thread.Sleep(100);

                var msmGenerator = new MessagesGenerator(1);
                var msmTournOff = msmGenerator.GenerateMessageEncender(0, 85);


                if (this._serviceTcp.IsConnected == true)
                {
                    result = this._serviceTcp.SendAndReceive(msmTournOff, 49);
                    //this._serviceTcp.Send(msmTournOff);
                    step = true;
                }
                else
                {
                    Console.WriteLine("Primer mensaje fallo");
                    step = false;
                }

                Thread.Sleep(200);
                if (this._serviceTcp.IsConnected == true && step == true)
                {
                    Console.WriteLine("mensaje de get id");
                    //result = this._serviceTcp.SendAndReceive(messageToSend, 84);
                   result = this._serviceTcp.SendAndReceive(messageToSend);
                    ResultGetIdRadar = result;
                    idRadar = result[4];
                    IdRadar = idRadar;
                }
                else
                {
                    Console.WriteLine("estado de la conexion: " + this._serviceTcp.IsConnected);
                    Console.WriteLine("el resultado es: " + result[0]);
                    erroCode = "\nFallo la conexión ";
                    step = false;
                }

                Thread.Sleep(200);
                if (result[0] == ListMessages.headerMessages["R_IdRadar"] && step == true)
                {                   
                    Console.WriteLine("R_IdRadar header = " + result[0]);                    
                    messagesGenerator = new MessagesGenerator(4);
                    messageToSend = messagesGenerator.GenerateMessageTime(idRadar);         
                    result = this._serviceTcp.SendAndReceive(messageToSend);
                   // result = this._serviceTcp.SendAndReceive(messageToSend,53);

                }
                else
                {
                    Console.WriteLine("estado de la conexion: " + this._serviceTcp.IsConnected);
                    Console.WriteLine("el resultado es: " + result[0]);
                    step = false;
                }


                Thread.Sleep(100);
                if (result[0] == ListMessages.headerMessages["RC_Hora"] && step == true)
                {
                    Console.WriteLine("R_GetStatus header = " + result[0]);
                    messagesGenerator = new MessagesGenerator(1);
                    messageToSend = messagesGenerator.GenerateMessageTXPower((byte)this._txPower, idRadar);
                    result = this._serviceTcp.SendAndReceive(messageToSend);
                    //result = this._serviceTcp.SendAndReceive(messageToSend,50);


                }
                else if (erroCode == "")
                {
                    Console.WriteLine("estado de la conexion: " + this._serviceTcp.IsConnected);
                    Console.WriteLine("el resultado es: " + result[0]);
                    erroCode = "\nFallo en el mensaje de SetTimeStamp ";
                    step = false;
                }

                Thread.Sleep(100);
                if (result[0] == ListMessages.headerMessages["RC_PotenciaRadar"] && step == true)
                {
                    Console.WriteLine("RC_PotenciaRadar header = " + result[0]);
                    messagesGenerator = new MessagesGenerator(1);
                    messageToSend = messagesGenerator.GenerateMessageTXChannel((byte)this._channel, idRadar);
                    result = this._serviceTcp.SendAndReceive(messageToSend);

                }
                else if (erroCode == "")
                {
                    Console.WriteLine("el resultado es: " + result[0]);
                    erroCode = "\nFallo en el mensaje de SetTxPower ";
                    step = false;
                }

                Thread.Sleep(100);
                if (result[0] == ListMessages.headerMessages["RC_CanalFrecRadar"] && step == true)
                {

                    Console.WriteLine("Conexion realizada satisfactoriamente ConnectDone");
                    Status = this._serviceTcp.Status;


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
                    this._serviceTcp.DisconnectedRadar();

                }

                return step;
            }
            else { return false; }
        }
    }

    public class Plots
    {
        public int RadarId { get; set; }
        public double Range { get; set; }
        public double Azimuth { get; set; }
        public int Power { get; set; }
        public DateTime TimeStamp { get; set; }

    }

    /// <summary>
    /// Clase Tracks, contiene la estructura de los tracks
    /// </summary>
    public class Tracks
    {
        public int Id { get; set; }
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
