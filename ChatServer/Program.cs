using System;
using System.Threading;

namespace ChatServer
{
    internal class Program
    {
        private static Thread _listenThread;
        private static void Main(string[] args)
        {
            using var server = new ChatServer();
            try
            {
                _listenThread = new Thread(server.Listen);
                _listenThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}