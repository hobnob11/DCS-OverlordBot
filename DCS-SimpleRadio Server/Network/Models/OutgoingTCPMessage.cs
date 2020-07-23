using Ciribob.DCS.SimpleRadio.Standalone.Common.Network;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Ciribob.DCS.SimpleRadio.Standalone.Server.Network.Models
{
    public class OutgoingTCPMessage
    {
        public NetworkMessage NetworkMessage {
            get;
            set;
        }

        public List<Socket> SocketList { get; set; }

    }
}
