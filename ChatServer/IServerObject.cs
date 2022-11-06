using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatServer
{
    public interface IServerObject
    {
        static TcpListener TcpListener;
        List<IClientObject> Clients {get;}
    }
}