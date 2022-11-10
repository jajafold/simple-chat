using System.Net.Sockets;

namespace ChatServer
{
    public interface IClientObject
    {
        string Id { get; set; }
        string UserName { get; set; }
        NetworkStream Stream { get; }
        void Close();
        void Process();
    }
}