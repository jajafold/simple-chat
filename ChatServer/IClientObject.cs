using System.Net.Sockets;

namespace ChatServer
{
    public interface IClientObject
    {
        string Id { get; set; }
        NetworkStream Stream { get; }
        void Close();
        void Process();
    }
}