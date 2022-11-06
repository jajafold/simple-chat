using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatClient
{
    public class Client : IClient
    {
        public string Name { get; }
        public string Host { get; }
        public int Port { get; }
        public TcpClient TcpClient { get; }
        public NetworkStream NetworkStream { get; private set; }

        public Client(string host, int port, string name = "")
        {
            Name = name == "" ? $"Guest#{host.GetHashCode() % 1000}" : name;
            
            Host = host;
            Port = port;
            TcpClient = new TcpClient();

            Connect(host, port);
        }

        private void Connect(string host, int port)
        {
            try
            {
                TcpClient.Connect(host, port);
                NetworkStream = TcpClient.GetStream(); 
                
                var data = Encoding.Unicode.GetBytes(Name ?? string.Empty);
                NetworkStream.Write(data, 0, data.Length);
                
                var receiveThread = new Thread(Receive);
                receiveThread.Start(); //старт потока
                Console.WriteLine($"Добро пожаловать, {Name}");
                Send("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Disconnect();
            }
        }

        public void Send(string msg)
        {
            Console.WriteLine("Введите сообщение: ");
            
            while (true)
            {
                var data = Encoding.Unicode.GetBytes(Console.ReadLine() ?? string.Empty);
                NetworkStream.Write(data, 0, data.Length);
            }
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
                        Console.WriteLine("Подключение прервано!");
                        Console.ReadLine();
                        Disconnect();
                        throw;
                    }
                    
                } while (NetworkStream.DataAvailable);
            
                Console.WriteLine(builder.ToString());
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