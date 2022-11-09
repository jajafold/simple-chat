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
        private static TcpListener TcpListener { get; set; }
        public List<IClientObject> Clients { get; } = new();

        void IServerObject.AddConnection(IClientObject clientObject)
        {
            Clients.Add(clientObject);
        }

        void IServerObject.RemoveConnection(string id)
        {
            var client = Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                Clients.Remove(client);
        }

        void IServerObject.BroadcastMessage(Message message)
        {
            var data = Encoding.Unicode.GetBytes(message.ToString());
            foreach (var t in Clients)
                t.Stream.Write(data, 0, data.Length);
        }

        protected internal void Listen()
        {
            try
            {
                TcpListener = new TcpListener(IPAddress.Any, 8888);
                TcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    var tcpClient = TcpListener.AcceptTcpClient();

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
            TcpListener.Stop();

            foreach (var t in Clients) t.Close();

            Environment.Exit(0);
        }
    }
}