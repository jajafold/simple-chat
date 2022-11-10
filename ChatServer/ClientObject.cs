using System;
using System.Net.Sockets;
using System.Text;
using ChatServer.Response;

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
        private IServerObject Server { get; }
        public string UserName { get; set; }

        public string Id { get; set; }
        public NetworkStream Stream { get; set; }

        public void Process()
        {
            try
            {
                Stream = Client.GetStream();
                var message = GetMessage();
                UserName = message.Text;

                var messageHello = new TextMessage($"{UserName} вошел в чат", DateTime.Now, "Server", "Server");
                Server.BroadcastResponse(new BasicResponse(messageHello));
                Console.WriteLine(messageHello.ToFlatString());
                while (true)
                    try
                    {
                        var messageSay = GetMessage();
                        Console.WriteLine(messageSay.ToFlatString());
                        Server.BroadcastResponse(new BasicResponse(messageSay));
                    }
                    catch (Exception e)
                    {
                        var messageBye = new TextMessage
                            ($"{UserName} покинул чат", DateTime.Now, "Server", "Server");
                        Console.WriteLine(messageBye.ToFlatString());
                        Server.BroadcastResponse(new UsersOnlineResponse(messageBye, Server.GetUserNamesOnline()));
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
            Stream?.Close();
            Client?.Close();
        }

        private TextMessage GetMessage()
        {
            var data = new byte[64];
            var builder = new StringBuilder();
            do
            {
                var bytes = Stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (Stream.DataAvailable);
            
            return builder.ToString() == "" ? throw new Exception(): new TextMessage
                (builder.ToString(), DateTime.Now, Id, UserName);
        }
    }
}