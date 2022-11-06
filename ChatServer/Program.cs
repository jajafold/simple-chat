using System;
using System.Threading;

namespace ChatServer
{
    internal class Program
    {
        private static ChatServer server;
        private static Thread listenThread;

        private static void Main(string[] args)
        {
            try
            {
                server = new ChatServer();
                listenThread = new Thread(server.Listen);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}