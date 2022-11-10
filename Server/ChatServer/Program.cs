using System;
using System.Threading;

namespace ChatServer
{
    internal class Program
    {
        private static ChatServer _server;
        private static Thread _listenThread;

        private static void Main(string[] args)
        {
            using var server = new ChatServer();
            try
            {
                _server = new ChatServer();
                _listenThread = new Thread(_server.Listen);
                _listenThread.Start();
            }
            catch (Exception ex)
            {
                _server.Disconnect();
                Console.WriteLine(ex.Message);
            }
        }
    }
}