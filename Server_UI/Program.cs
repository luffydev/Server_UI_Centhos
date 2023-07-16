using SDK.Network;
using SDK.Singleton;

namespace Server_UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Singleton<OpcodeStore>.Instance.BuildOpcodeList();



            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new loginForm());
        }
    }
}