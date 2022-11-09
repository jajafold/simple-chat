using System.Net.Sockets;

namespace Chat.Application
{
    public interface IClient
    {
        public string Name { get; }
        public string Host { get; }
        public int Port { get; }
        public TcpClient TcpClient { get; }
        public NetworkStream NetworkStream { get; }
        
        public void Send(string msg);
        public void Receive();
        public void Disconnect();
    }
}