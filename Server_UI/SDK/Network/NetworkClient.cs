using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

using SDK.Singleton;

namespace SDK.Network 
{
    public class NetworkClient : Singleton<NetworkClient>
    {
        private bool mConnected = false;
        private Socket mSocket = null;
        private byte[] mBuffer = new byte[1024];
        
        //ctor
        public NetworkClient()
        {
        }

        public void Connect(String pHost, int pPort)
        {
            // If we are not connected yet
            if(!IsConnected())
            {
                try
                {
                    mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    mSocket.Connect(new IPEndPoint(IPAddress.Parse(pHost), pPort));

                    mSocket.BeginReceive(mBuffer, 0, mBuffer.Length, SocketFlags.None, ReceiveCallback, null);


                    mConnected = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Erreur de la connexion au serveur : {0}", ex.Message);
                }

                
            }
        }

        private void ReceiveCallback(IAsyncResult pResult)
        {
            try
            {

                int lReceivedBytes = mSocket.EndReceive(pResult);

                if (lReceivedBytes > 0 && (lReceivedBytes > (sizeof(UInt32) * 2)))
                {
                    byte[] lReceivedData = new byte[lReceivedBytes];
                    Array.Copy(mBuffer, lReceivedData, lReceivedBytes);

                    NetworkPacket lPacket = new NetworkPacket(mBuffer);

                    mSocket.BeginReceive(mBuffer, 0, mBuffer.Length, SocketFlags.None, ReceiveCallback, null);

                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Send(byte[] pData)
        {
            try
            {
                mSocket.Send(pData);
                Console.WriteLine("Données envoyées au serveur (bytes)");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'envoi des données au serveur : {0}", ex.Message);
            }
        }
        public void Disconnect()
        {
            try
            {
                // Fermer la connexion avec le serveur
                mSocket.Close();
                mConnected = false;

                Console.WriteLine("Déconnecté du serveur");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la déconnexion : {0}", ex.Message);
            }
        }

        public bool IsConnected()
        {
            return mConnected;
        }
    }
}
