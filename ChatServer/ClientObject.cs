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
                UserName = message.Text;

                var messageHello = new Message($"{UserName} вошел в чат", DateTime.Now, "Server", "Server");
                Server.BroadcastMessage(messageHello);
                Console.WriteLine(messageHello);
                while (true)
                    try
                    {
                        var messageSay = GetMessage();
                        Console.WriteLine(messageSay);
                        Server.BroadcastMessage(messageSay);
                    }
                    catch (Exception e)
                    {
                        var messageBye = new Message
                            ($"{UserName} покинул чат", DateTime.Now, "Server", "Server");
                        Console.WriteLine(messageBye);
                        Server.BroadcastMessage(messageBye);
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

        private Message GetMessage()
        {
            var data = new byte[64];
            var builder = new StringBuilder();
            do
            {
                var bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);
            
            return builder.ToString() == "" ? throw new Exception(): new Message
                (builder.ToString(), DateTime.Now, Id, UserName);
        }
    }
}