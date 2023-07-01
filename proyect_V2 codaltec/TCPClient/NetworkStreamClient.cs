using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPClient
{
    class NetworkStreamClient
    {
        public Guid ClientId { get; private set; }
        public Socket Socket_ { get; private set; }
        public IPEndPoint EndPoint { get; private set; }
        public bool IsGuidAssigned { get; private set; }
        public IPAddress Address { get; private set; }


        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
            new ManualResetEvent(false);


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


        public NetworkStreamClient(string address, int port)
        {
            IPAddress ipAddress;
            var validIp = IPAddress.TryParse(address, out ipAddress);

            if (!validIp)
                ipAddress = Dns.GetHostAddresses(address)[0];

            Address = ipAddress;
            EndPoint = new IPEndPoint(ipAddress, port);
            Socket_ = new Socket(AddressFamily.InterNetwork,
                              SocketType.Stream, ProtocolType.Tcp);

            ReceiveBufferSize = 256;
            SendBufferSize = 256;
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

        private  void TrysendBuffer()
        {
            try
            {
                using (Stream s = new NetworkStream(Socket_))
                {
                    byte[] myWriteBuffer = Encoding.ASCII.GetBytes("Are you receiving this message?");
                    s.BeginWrite(myWriteBuffer, 0, myWriteBuffer.Length,new AsyncCallback(MyWriteCallBack),s);
                    sendDone.WaitOne();                    
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("TrySendObject " + e.Message);
                
            }
        }

        public static void MyWriteCallBack(IAsyncResult ar)
        {

            NetworkStream myNetworkStream = (NetworkStream)ar.AsyncState;
            myNetworkStream.EndWrite(ar);
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


    }
}
