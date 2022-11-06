using System.Net.Sockets;

namespace ChatServer
{
    public interface IClientObject
    {
        string Id { get; set; }
        void Close();
        NetworkStream Stream { get; }
        IServerObject ServerObject { get; }
    }
}