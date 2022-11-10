using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServer
{
    public class ChatServer : IServerObject, IDisposable
    {
        private static TcpListener TcpListener { get; set; }
        private Dictionary<string, IClientObject> Clients { get; } = new ();

        void IServerObject.AddConnection(IClientObject clientObject)
        {
            Clients[clientObject.Id] = clientObject;
        }

        void IServerObject.RemoveConnection(string id)
        {
            Clients.Remove(id);
        }

        void IServerObject.BroadcastMessage(Message message)
        {
            var data = Encoding.Unicode.GetBytes(message.ToFlatString());
            foreach (var kvp in Clients)
                kvp.Value.Stream.Write(data, 0, data.Length);
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

        private void Disconnect()
        {
            if (TcpListener != null)
                TcpListener.Stop();

            foreach (var t in Clients) t.Value.Close();

            Environment.Exit(0);
        }

        public void Dispose()
        {
            //Disconnect();
        }
    }
}