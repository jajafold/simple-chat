using System.Collections.Generic;
using System.Net.Sockets;
using ChatServer.Response;

namespace ChatServer
{
    public interface IServerObject
    {
        internal void AddConnection(IClientObject clientObject);
        internal void RemoveConnection(string id);
        internal void BroadcastResponse<TResponseType,TMessage>(TResponseType response) 
            where TMessage : Message
            where TResponseType : IResponse<TMessage>;
        IEnumerable<string> GetUserNamesOnline();
    }
}