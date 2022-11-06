﻿using System;
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

        public IWriter _writer;

        public Client(string host, int port, IWriter writer, string name = "")
        {
            Name = name == "" ? $"Guest#{host.GetHashCode() % 1000}" : name;
            
            Host = host;
            Port = port;
            TcpClient = new TcpClient();
            _writer = writer;
        }

        public void Connect()
        {
            try
            {
                TcpClient.Connect(Host, Port);
                NetworkStream = TcpClient.GetStream(); 
                
                var data = Encoding.Unicode.GetBytes(Name ?? string.Empty);
                NetworkStream.Write(data, 0, data.Length);
                
                var receiveThread = new Thread(Receive);
                receiveThread.Start();
                _writer.WriteLine($"Добро пожаловать, {Name}");
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
                        _writer.WriteLine("Подключение прервано!");
                        Disconnect();
                        throw;
                    }
                    
                } while (NetworkStream.DataAvailable);

                _writer.WriteLine(builder.ToString());
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