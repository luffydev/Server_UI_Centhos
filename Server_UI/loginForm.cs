using SDK.Network;
using SDK.Singleton;
using System.Security.Cryptography;
using System.Text;

namespace Server_UI
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            NetworkClient lClient = Singleton<NetworkClient>.Instance;
            lClient.Connect("127.0.0.1", 3350);

            if (lClient.IsConnected())
            {
                NetworkPacket lPacket = new NetworkPacket(Opcodes.CMSG_CONNECT_CHALLENGE_REQUEST);

                lPacket.WriteString(usernameField.Text);

                using (SHA512 lSha = SHA512.Create())
                {

                    byte[] lHash = lSha.ComputeHash(Encoding.UTF8.GetBytes(usernameField.Text + ":" + passwordField.Text));

                    StringBuilder builder = new StringBuilder();

                    for (int i = 0; i < lHash.Length; i++)
                    {
                        builder.Append(lHash[i].ToString("x2"));
                    }

                    lPacket.WriteBytes(lHash);
                }

                lClient.Send(lPacket.Build());
            }
        }
    }
}