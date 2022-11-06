using System;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class ClientObject : IClientObject
    {
        private readonly TcpClient client;
        private readonly IServerObject server; // объект сервера
        private string userName;

        public ClientObject(TcpClient tcpClient, IServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            client = tcpClient;
            server = serverObject;
            serverObject.AddConnection(this);
        }

        public string Id { get; set; }
        public NetworkStream Stream { get; set; }
        public IServerObject ServerObject { get; }

        public void Process()
        {
            try
            {
                Stream = client.GetStream();
                var message = GetMessage();
                userName = message;

                message = userName + " вошел в чат";
                server.BroadcastMessage(message, Id);
                Console.WriteLine(message);
                while (true)
                    try
                    {
                        message = GetMessage();
                        message = string.Format("{0}: {1}", userName, message);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, Id);
                    }
                    catch
                    {
                        message = string.Format("{0}: покинул чат", userName);
                        Console.WriteLine(message);
                        server.BroadcastMessage(message, Id);
                        break;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                server.RemoveConnection(Id);
                Close();
            }
        }

        public void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }

        private string GetMessage()
        {
            var data = new byte[64];
            var builder = new StringBuilder();
            var bytes = 0;
            do
            {
                bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);

            return builder.ToString();
        }
    }
}