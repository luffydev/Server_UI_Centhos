
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

using SDK.Singleton;

namespace SDK.Network 
{
    public class NetworkClient : Singleton<NetworkClient>
    {
        private bool mConnected = false;
        private Socket mSocket;
        private byte[] mBuffer = new byte[1024];
        
        //ctor
        public NetworkClient()
        {
            mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Connect(String pHost, int pPort)
        {
            // If we are not connected yet
            if(!IsConnected())
            {
                try
                {
                    mSocket.Connect(new IPEndPoint(IPAddress.Parse(pHost), pPort));
                    mSocket.BeginReceive(mBuffer, 0, mBuffer.Length, SocketFlags.None, ReceiveCallback, null);


                    mConnected = true;
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(("Erreur de la connexion au serveur : " + ex.Message));
                }

                
            }
        }

        private void ReceiveCallback(IAsyncResult pResult)
        {
            try
            {

                int lReceivedBytes = mSocket.EndReceive(pResult);

                if ( lReceivedBytes > 0 && (lReceivedBytes >= (sizeof(UInt32) * 2) + sizeof(UInt64)) )
                {
                    byte[] lReceivedData = new byte[lReceivedBytes];
                    Array.Copy(mBuffer, lReceivedData, lReceivedBytes);

                    NetworkPacket lPacket = new NetworkPacket(mBuffer);

                    OpcodeStore lStore = Singleton<OpcodeStore>.Instance;

                    if(lStore.OpcodeExist((uint)lPacket.GetOpcode()))
                    {
                        OpcodeStruct lStruct = lStore.GetOpcodeStruct((uint)lPacket.GetOpcode());
                        lStruct.handler(lPacket, this);
                    }

                    mSocket.BeginReceive(mBuffer, 0, mBuffer.Length, SocketFlags.None, ReceiveCallback, null);

                }
            }
            catch (Exception)
            {
            }
        }

        public void Send(byte[] pData)
        {
            try
            {
                mSocket.Send(pData);
                Trace.WriteLine(("Données envoyées au serveur : " + pData.Length.ToString() + " bytes"));

            }
            catch (Exception ex)
            {
                Trace.WriteLine(("Erreur lors de l'envoi des données au serveur : " + ex.Message));
            }
        }
        public void Disconnect()
        {
            try
            {
                // Fermer la connexion avec le serveur
                mSocket.Close();
                mConnected = false;

                Trace.WriteLine("Déconnecté du serveur");
            }
            catch (Exception ex)
            {
                Trace.WriteLine(("Erreur lors de la déconnexion : " + ex.Message));
            }
        }

        public bool IsConnected()
        {
            return mConnected;
        }
    }
}
