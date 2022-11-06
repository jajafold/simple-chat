using System.Net.Sockets;

namespace ChatServer
{
    public interface IClientObject
    {
        string Id { get; set; }
        NetworkStream Stream { get; }
        IServerObject ServerObject { get; }
        void Close();
        void Process();
    }
}