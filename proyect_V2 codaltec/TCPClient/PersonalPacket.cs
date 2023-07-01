using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPClient
{
    [Serializable]
    public class PersonalPacket
    {
        public string GuidId { get; set; }
        public object Package { get; set; }
    }

    [Serializable]
    public class PingPacket
    {
        public string GuidId { get; set; }
    }

    public class PacketEvents : EventArgs
    {
        public TCPAsynClient Sender;
        public TCPAsynClient Receiver;
        public object Packet;
    }

    public class PersonalPacketEvents : EventArgs
    {
        public TCPAsynClient Sender;
        public TCPAsynClient Receiver;
        public PersonalPacket Packet;
    }
}
