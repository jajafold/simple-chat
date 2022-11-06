using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    public class ChatServer : IServerObject
    {
        private static TcpListener tcpListener;
        public List<IClientObject> Clients { get; } = new();

        public void AddConnection(IClientObject clientObject)
        {
            Clients.Add(clientObject);
        }

        public void RemoveConnection(string id)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                Clients.Remove(client);
        }

        public void BroadcastMessage(string message, string id)
        {
            var data = Encoding.Unicode.GetBytes(message);
            foreach (var t in Clients.Where(t => t.Id != id)) t.Stream.Write(data, 0, data.Length);
        }

        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    var tcpClient = tcpListener.AcceptTcpClient();

                    IClientObject clientObject = new ClientObject(tcpClient, this);
                    var clientThread = new Thread(clientObject.Process);
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        protected internal void Disconnect()
        {
            tcpListener.Stop();

            foreach (var t in Clients) t.Close();

            Environment.Exit(0);
        }
    }
}