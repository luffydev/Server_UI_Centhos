using SDK.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDK.Network
{
    public class NetworkHandler
    {
        public static void Handle_NULL(NetworkPacket pPacket, NetworkClient pClient)
        {
            Console.WriteLine("Handle_NULL: Packet received - OpCode: {0}, Client: {1}", pPacket.GetOpcode(), pClient);
        }

        public static void Handle_Connect_Challenge_Response(NetworkPacket pPacket, NetworkClient pClient)
        {

        }
    }
}
