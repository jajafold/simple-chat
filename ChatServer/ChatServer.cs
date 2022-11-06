using System.Collections.Generic;
using System.Net.Sockets;

namespace ChatServer
{
    public class ChatServer
    {
        static TcpListener tcpListener;
        List<ClientObject> clients = new List<ClientObject>();
 
        protected internal void AddConnection(ClientObject clientObject)
        {
            clients.Add(clientObject);
        }
        protected internal void RemoveConnection(string id)
        {
            // получаем по id закрытое подключение
            ClientObject client = clients.FirstOrDefault(c => c.Id == id);
            // и удаляем его из списка подключений
            if (client != null)
                clients.Remove(client);
        }
}