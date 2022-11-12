using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Infrastructure;
using Infrastructure.Response;
using Ninject;

namespace Chat.Domain
{
    public class Client : IClient
    {
        public string Name { get; }
        public string Host { get; }
        public int Port { get; }
        public TcpClient TcpClient { get; }
        public NetworkStream NetworkStream { get; private set; }

        private readonly IWriter _writer;
        private readonly Thread _receiveThread;

        public Client(string host, int port, string name = "")
        {
            Host = host;
            Port = port;
            Name = name;
            _writer = DependencyInjector.Injector.Get<Writer>(); //TODO: Реализовать через IChat

            TcpClient = new TcpClient();
            _receiveThread = new Thread(Receive);
        }

        public void Connect()
        {
            try
            {
                TcpClient.Connect(Host, Port);
                NetworkStream = TcpClient.GetStream(); 
                
                var data = Encoding.Unicode.GetBytes(Name ?? string.Empty);
                NetworkStream.Write(data, 0, data.Length);
                
                _receiveThread.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Disconnect();
            }
        }

        public void Send(string msg)
        {
            var data = Encoding.Unicode.GetBytes(msg);
            NetworkStream.Write(data, 0, data.Length);
        }

        public void Receive()
        {
            while (true)
            {
                var data = new byte[64];
                var builder = new StringBuilder();

                do
                {
                    try
                    {
                        var bytes = NetworkStream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    catch
                    {
                        _writer.Write("Подключение прервано!");
                        Disconnect();
                        throw;
                    }
                } while (NetworkStream.DataAvailable);
                
                //get writer
                var jsonString = builder.ToString();
                var serialized = Deserializer.Deserialize(jsonString);
                var response = Convert.ChangeType(serialized.Obj, serialized.Type);

                if (response is UsersOnlineResponse onlineResponse)
                {
                    response = onlineResponse;
                }

                if (response is BasicResponse basicResponse)
                {
                    _writer.Write(basicResponse.Message.ToFlatString());
                }
                    
            }
        }

        public void Disconnect()
        {
            NetworkStream?.Close();
            TcpClient?.Close();
            
            Environment.Exit(0);
        }
    }
}