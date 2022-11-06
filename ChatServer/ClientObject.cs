using System;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class ClientObject : IClientObject
    {
        public ClientObject(TcpClient tcpClient, IServerObject serverObject)
        {
            Id = Guid.NewGuid().ToString();
            Client = tcpClient;
            Server = serverObject;
            serverObject.AddConnection(this);
        }

        private TcpClient Client { get; }
        private IServerObject Server { get; } // объект сервера
        private string UserName { get; set; }

        public string Id { get; set; }
        public NetworkStream Stream { get; set; }

        public void Process()
        {
            try
            {
                Stream = Client.GetStream();
                var message = GetMessage();
                UserName = message;

                message = UserName + " вошел в чат";
                Server.BroadcastMessage(message, Id);
                Console.WriteLine(message);
                while (true)
                    try
                    {
                        message = GetMessage();
                        message = string.Format("{0}: {1}", UserName, message);
                        Console.WriteLine(message);
                        Server.BroadcastMessage(message, Id);
                    }
                    catch
                    {
                        message = string.Format("{0}: покинул чат", UserName);
                        Console.WriteLine(message);
                        Server.BroadcastMessage(message, Id);
                        break;
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Server.RemoveConnection(Id);
                Close();
            }
        }

        public void Close()
        {
            if (Stream != null)
                Stream.Close();
            if (Client != null)
                Client.Close();
        }

        private string GetMessage()
        {
            var data = new byte[64];
            var builder = new StringBuilder();
            do
            {
                var bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);

            return builder.ToString();
        }
    }
}