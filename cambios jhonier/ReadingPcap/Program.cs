using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Timers;
using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;

namespace ReadingPcap
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenido al reproductor de Pcap");
            Console.WriteLine("De la siguiente lista seleccione el tipo de paquete quiere probar.");
            Console.WriteLine("1) Plots Random");
            Console.WriteLine("2) Plots continuos");
            Console.WriteLine("3) Tracks Random");
            Console.WriteLine("4) Tracks continuos");

            var a = Console.ReadKey();
            char b;
            if (char.IsDigit(a.KeyChar))
            {
                b = a.KeyChar;
            }
            else { b = '1'; }
            Console.WriteLine(b);
            StarClient starClient;
            starClient = new StarClient(b);

            starClient.Connect();

            Console.ReadLine();
        }

    }

    public class StarClient
    {
        #region
        private const int port = 7023;

        public static ManualResetEvent allDone = new ManualResetEvent(false);


        private static String response = String.Empty;
        public byte[] bytes { get; set; }
        public static Socket listener { get; set; }
        static Socket handler { get; set; }

        public static string data = null;

        private static byte[] setTimeStampMsm = { 37, 251, 251, 1, 1, 255 };

        //private static byte[] setTimeStampResp = { 53, 85, 251, 1, 1, 255 };
        private static byte[] setTimeStampResp = { 53, 85, 251, 1, 1, 255 };

        private static byte[] txpowerMsm = { 34, 251, 251, 1, 10, 255 };

        private static byte[] txpowerDeviceResp = { 50, 85, 251, 1, 100, 255 };

        private static byte[] channelFrecResp = { 51, 85, 251, 1, 1, 255 };

        private static byte[] getIdResp = { 84, 85, 251, 1, 85, 255 };




        private static byte[] channelFrecMsm = { 35, 251, 251, 1, 1, 255 };

        private static byte[] getIdMsm = { 68, 251, 0, 1, 1, 255 };

        private static byte[] radiationresponsed = { 49, 85, 251, 1, 1, 255 };

        static int m_packetNumber = 0;

        public static OfflinePacketDevice selectedDevice;
        static int readWholePacket;
        public static int readTimeOut;
        #endregion
        public StarClient(char t)
        {
            string _file;
            string _filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            switch (t)
            {
                case '1':
                    _file = "plots_nuevos.pcap";
                    Console.WriteLine("Ha seleccionado {0} se reproducira el archivo {1} con los datos grabados", t, _file);
                    _filePath = Path.Combine(_filePath, _file);
                    Console.WriteLine(_filePath);
                    break;
                case '2':
                    _file = "plots_continuos.pcap";
                    _filePath = Path.Combine(_filePath, _file);
                    Console.WriteLine("Ha seleccionado {0} se reproducira el archivo {1} con los datos grabados", t, _file);
                    break;
                case '3':
                    _file = "tracks_individuales.pcap";
                    _filePath = Path.Combine(_filePath, _file);
                    Console.WriteLine("Ha seleccionado {0} se reproducira el archivo {1} con los datos grabados", t, _file);
                    break;
                case '4':
                    _file = "tracks_continuos.pcap";
                    _filePath = Path.Combine(_filePath, _file);
                    Console.WriteLine("Ha seleccionado {0} se reproducira el archivo {1} con los datos grabados", t, _file);
                    break;
                default:
                    _file = "plots_nuevos.pcap";
                    _filePath = Path.Combine(_filePath, _file);
                    Console.WriteLine("Ha seleccionado {0} se reproducira el archivo {1} con los datos grabados", t, _file);
                    break;

            }

            // Create the offline device
            selectedDevice = new OfflinePacketDevice(_filePath);

            // 65536 guarantees that the whole packet will be captured on all the link layers
            int readWholePacket = 65536;

            // read timeout
            readTimeOut = 1000;
        }



        public void Connect()
        {

            bool count = true;
            bytes = new byte[256];
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = IPAddress.Parse("192.168.1.100");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);


            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.  
                    while (count == true)
                    {
                        int bytesRec = handler.Receive(bytes);
                        Console.WriteLine(bytes[0]);

                        if (bytes[0] == getIdMsm[0] && handler.Connected == true)
                        {
                            handler.Send(getIdResp);
                            Console.WriteLine("SetIdRespond");

                        }
                        if (bytes[0] == setTimeStampMsm[0])
                        {
                            handler.Send(setTimeStampResp);
                            Console.WriteLine("setTimeStampResp");
                        }

                        if (bytes[0] == txpowerMsm[0])
                        {
                            handler.Send(txpowerDeviceResp);
                            Console.WriteLine("txpowerDeviceResp");
                        }
                        if (bytes[0] == channelFrecMsm[0])
                        {
                            handler.Send(channelFrecResp);
                            Console.WriteLine("channelFrecResp");

                        }
                        if (bytes[0] == 33)
                        {
                            handler.Send(radiationresponsed);
                            Console.WriteLine("channelFrecResp");
                            count = false;
                            break;
                        }

                    }

                    OnTimedEvent();


                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void OnTimedEvent()
        {
            using (PacketCommunicator communicator = selectedDevice.Open(readWholePacket, PacketDeviceOpenAttributes.Promiscuous, readTimeOut))
            {

                communicator.ReceivePackets(0, -IncommingPacketHandler);
            }
        }

        public static void sendPlots(Packet packet)
        {
            int lenghtdata = packet.Ethernet.PayloadLength - 1;
            var buffer = packet.Buffer;
            var ttl = packet.IpV4.Ttl + 1;
            var total = buffer.Length - ttl;
            var data = ArraySub.SubArray(buffer, ttl, total);
            try
            {
                handler.Send(data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Disconect()
        {
            try
            {
                listener.Shutdown(SocketShutdown.Both);
                listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        private static void IncommingPacketHandler(Packet packet)
        {
            Thread.Sleep(2000);
            // This function will get called for every packet in the .pcap file!
            m_packetNumber++;

            Console.WriteLine(packet.Timestamp.ToString("yyyy-MM-dd hh:mm:ss.fff") + " length:" + packet.Length);



            if (packet.Ethernet.IpV4.Tcp.IsReset == false)
            {
                IpV4Datagram ip = packet.Ethernet.IpV4;
                UdpDatagram udp = ip.Udp;
                sendPlots(packet);
                // print ip addresses and udp ports
                Console.WriteLine(ip.Source + ":" + udp.SourcePort + " -> " + ip.Destination + ":" + udp.DestinationPort);

            }


            if (packet is null)
            {

                Disconect();
            }
            Console.WriteLine();

        }

    }

    public static class ArraySub
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }


}

