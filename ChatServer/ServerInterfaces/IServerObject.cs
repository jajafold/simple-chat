using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatServer
{
    public interface IServerObject
    {
        internal void AddConnection(IClientObject clientObject);
        internal void RemoveConnection(string id);
        internal void BroadcastMessage(Message message);
    }
}