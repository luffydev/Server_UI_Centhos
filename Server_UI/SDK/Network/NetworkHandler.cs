using System.Diagnostics;

namespace SDK.Network
{
    public class NetworkHandler
    {
        public static void Handle_NULL(NetworkPacket pPacket, NetworkClient pClient)
        {
            Trace.WriteLine(("Handle_NULL: Packet received - OpCode: ", pPacket.GetOpcode().ToString()));
        }

        public static void Handle_Connect_Challenge_Response(NetworkPacket pPacket, NetworkClient pClient)
        {
            Trace.WriteLine("RECV Handle_Connect_Challenge_Response let's handle it");

            byte lStatus = pPacket.ReadByte();
            String lKey = pPacket.ReadString();

            Trace.WriteLine("TEST : " + lKey);
        }
    }
}
