using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPClient
{
    public class TCPAsynClient
    {

        //// ManualResetEvent instances signal completion.  
        //private static ManualResetEvent connectDone =
        //    new ManualResetEvent(false);
        //private static ManualResetEvent sendDone =
        //    new ManualResetEvent(false);
        //private static ManualResetEvent receiveDone =
        //    new ManualResetEvent(false);

        // The response from the remote device.  
        private static String response = String.Empty;

        private int SizeDataReceived;
        public byte[] BufferReceived { get; private set; }

        public Guid ClientId { get; private set; }
        public static Socket Socket_ { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
        public bool IsGuidAssigned { get; private set; }
        public IPAddress Address { get; private set; }

        public bool IsConnected { get; private set; }

        public int ReceiveBufferSize
        {
            get { return Socket_.ReceiveBufferSize; }
            set { Socket_.ReceiveBufferSize = value; }
        }

        public int SendBufferSize
        {
            get { return Socket_.SendBufferSize; }
            set { Socket_.SendBufferSize = value; }
        }

        public TCPAsynClient(string address, int port)
        {
            IPAddress ipAddress;
            var validIp = IPAddress.TryParse(address, out ipAddress);

            if (!validIp)
                ipAddress = Dns.GetHostAddresses(address)[0];

            Address = ipAddress;
            EndPoint = new IPEndPoint(ipAddress, port);
            Socket_ = new Socket(AddressFamily.InterNetwork,
                              SocketType.Stream, ProtocolType.Tcp);


            Socket_.ReceiveBufferSize = 256;
            Socket_.SendBufferSize = 256;
            Socket_.NoDelay = true;
            
            Socket_.SendTimeout = 1000;
            Socket_.ReceiveTimeout = 1000;
        }


        //public async Task<byte[]> SendByte(byte[] obj)
        //{
        //    return await Task.Run(() => TrySendByte(obj));
        //}


        public async Task<byte[]> SendByte2(byte[] obj)
        {
            return await Task.Run(() => TrySendByte2(obj));
        }

        private byte[] TrySendByte2(byte[] data)
        {
            // Connect to a remote device.  
            try
            {
                BufferReceived = new byte[256];
                BufferReceived[0] = 255;
                SizeDataReceived = data.Length;
                // Send test data to the remote device.  
                // Send the data through the socket.  
                int bytesSent = Socket_.Send(data);
                int bytesRec = Socket_.Receive(BufferReceived);
                return BufferReceived;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return BufferReceived;
            }
        }

        //private byte[] TrySendByte(byte[] data)
        //{
        //    // Connect to a remote device.  
        //    try
        //    {
        //        Console.WriteLine("esto se esta utilizando");
        //        SizeDataReceived = data.Length;
        //        // Send test data to the remote device.  
        //        Send(Socket_, data);
        //        sendDone.WaitOne();

        //        // Receive the response from the remote device.  
        //        Receive(Socket_);
        //        receiveDone.WaitOne();
        //        return BufferReceived;

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return BufferReceived;
        //    }
        //}

        //private void Receive(Socket client)
        //{
        //    try
        //    {
        //        // Create the state object.  
        //        StateObject state = new StateObject();
        //        state.workSocket = client;

        //        // Begin receiving the data from the remote device.  
        //        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //            new AsyncCallback(ReceiveCallback), state);


        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        var error = new byte[1];
        //        error[0] = 0;

        //    }
        //}


        //public void ReceiveCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the state object and the client socket   
        //        // from the asynchronous state object.  
        //        StateObject state = (StateObject)ar.AsyncState;
        //        Socket client = state.workSocket;

        //        // Read data from the remote device.  
        //        int bytesRead = client.EndReceive(ar);

        //        if (bytesRead > 0)
        //        {
        //            state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
        //            BufferReceived = state.buffer;
        //            response = state.sb.ToString();
        //            if (SizeDataReceived == bytesRead)
        //            {
        //                state.sb.Clear();
        //                receiveDone.Set();
        //            }
        //            else
        //            {
        //                SizeDataReceived += bytesRead;
        //                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
        //                new AsyncCallback(ReceiveCallback), state);
        //            }
        //        }
        //        else
        //        {
        //            // All the data has arrived; put it in response.  
        //            if (state.sb.Length > 1)
        //            {
        //                response = state.sb.ToString();
        //            }
        //            // Signal that all bytes have been received.  
        //            receiveDone.Set();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

        //private static void Send(Socket client, byte[] data)
        //{

        //    // Begin sending the data to the remote device.  
        //    client.BeginSend(data, 0, data.Length, 0,
        //        new AsyncCallback(SendCallback), client);
        //}

        //private static void SendCallback(IAsyncResult ar)
        //{
        //    try
        //    {
        //        // Retrieve the socket from the state object.  
        //        Socket client = (Socket)ar.AsyncState;

        //        // Complete sending the data to the remote device.  
        //        int bytesSent = client.EndSend(ar);
        //        Console.WriteLine("Sent {0} bytes to server.", bytesSent);

        //        // Signal that all bytes have been sent.  
        //        sendDone.Set();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //}

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


        public TCPAsynClient()
        {

        }

        public async Task<bool> Connect()
        {
            var result = await Task.Run(() => TryConnect());
            string guid = string.Empty;

            try
            {
                if (result)
                {
                    guid = RecieveGuid();
                    ClientId = Guid.Parse(guid);
                    IsGuidAssigned = true;
                    return true;
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Connect " + e.Message);
            }

            return false;
        }

        public async Task<string> CreateGuid(Socket socket)
        {
            return await Task.Run(() => TryCreateGuid(socket));
        }


        public async Task<bool> SendObject(byte[] obj)
        {
            return await Task.Run(() => TrySendObject(obj));
        }


        public async Task<MensajeTrans> RecieveObject()
        {
            return await Task.Run(() => TryRecieveObject());
        }


        private string TryCreateGuid(Socket socket)
        {
            Socket_ = socket;
            var endPoint = ((IPEndPoint)Socket_.LocalEndPoint);
            EndPoint = endPoint;

            ClientId = Guid.NewGuid();
            return ClientId.ToString();
        }
        private MensajeTrans TryRecieveObject()
        {
            if (Socket_.Available == 0)
                return null;

            byte[] data = new byte[Socket_.ReceiveBufferSize];


            try
            {
                //Socket_.Receive(data, Socket_.Available,
                //                           SocketFlags.None);

                using (Stream s = new NetworkStream(Socket_))
                {


                    s.Read(data, 0, data.Length);
                   
                    s.Flush();
                    return new MensajeTrans
                    {
                        Mensaje = BitConverter.ToInt32(data, 0),
                        Size = data.Length,
                        Data = data

                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("TryRecieveObject " + e.Message);
                return null;
            }
        }

        private bool TrySendObject(byte[] obj)
        {
            try
            {
                using (Stream s = new NetworkStream(Socket_))
                {
                    var memory = new MemoryStream();
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(memory, obj);
                    var newObj = memory.ToArray();

                    memory.Position = 0;

                    s.Write(obj, 0, obj.Length);
                    return true;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("TrySendObject " + e.Message);
                return false;
            }
        }

        public bool TrySendMessage(string message)
        {
            try
            {
                using (Stream s = new NetworkStream(Socket_))
                {
                    StreamWriter writer = new StreamWriter(s);
                    writer.AutoFlush = true;

                    writer.WriteLine(message);
                    return true;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("TrySendMessage " + e.Message);
                return false;
            }
        }
        public string RecieveGuid()
        {
            try
            {
                using (Stream s = new NetworkStream(Socket_))
                {
                    //var reader = new StreamReader(s);
                    //s.ReadTimeout = 5000;
                    ClientId = Guid.NewGuid();
                    return ClientId.ToString();

                }
            }
            catch (IOException e)
            {
                Console.WriteLine("RecieveGuid " + e.Message);
                return null;
            }
        }

        private bool TryConnect()
        {
            try
            {
                Socket_.Connect(EndPoint);
                return true;
            }
            catch
            {
                Console.WriteLine("Connection failed.");
                return false;
            }
        }
        public async Task<bool> PingToRadar() 
        {

            try
            {
                return await Task.Run(() => TryPing());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
           
        }
            
        private bool TryPing()
        {

            bool rec = Socket_.Poll(-1, SelectMode.SelectError);
            return rec;
        }

        public async Task<bool> PingConnection()
        {
            try
            {
                var aa = new PingPacket();
                byte[] bytes = Encoding.ASCII.GetBytes(aa.ToString());

                //var result = await SendObject(new PingPacket());
                var result = await SendObject(bytes);
                return result;
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("IsSocketConnected " + e.Message);
                return false;
            }
        }
        public async Task<bool> TryScocktConnected()
        {
            return await Task.Run(()=> IsSocketConnected());

        }
        public bool IsSocketConnected()
        {
            try
            {
                bool part1 = Socket_.Poll(-1, SelectMode.SelectRead);
                bool part2 = (Socket_.Available == 0);
                if (part1 && part2)
                {    
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine("IsSocketConnected " + e.Message);
                return false;
            }
        }
        public void Disconnect()
        {
            Socket_.Close();
        }

    }

    public class MensajeTrans
    {
        public double Mensaje { get; set; }
        public int Size { get; set; }

        public byte[] Data { get; set; }
    }
}
