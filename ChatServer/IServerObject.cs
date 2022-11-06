using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatServer
{
    public interface IServerObject
    {
        static TcpListener TcpListener;
        List<IClientObject> Clients {get;}
        public void AddConnection(IClientObject clientObject);
        public void RemoveConnection(string id);
        public void BroadcastMessage(string message, string id);
    }
}