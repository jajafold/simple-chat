using System.Collections.Generic;
using System.Net.Sockets;
using ChatServer.Response;

namespace ChatServer
{
    public interface IServerObject
    {
        internal void AddConnection(IClientObject clientObject);
        internal void RemoveConnection(string id);
        internal void BroadcastResponse<TMessage>(IResponse<TMessage> response) where TMessage : Message;
        IEnumerable<string> GetUserNamesOnline();
    }
}