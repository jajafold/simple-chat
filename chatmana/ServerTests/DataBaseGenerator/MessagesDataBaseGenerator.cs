using Infrastructure;
using Infrastructure.Services;

namespace ServerTests;

public class MessagesDataBaseGenerator
{
    public readonly IServerDataBase DataBase;

    public MessagesDataBaseGenerator()
    {
        DataBase = new ServerDataBase();
        DataBase.Chatrooms[DataBase.MainChat].Users!
            .AddRange(new List<string> {"Gena", "Vasya", "Petya"});
        DataBase.Chatrooms.Add(Guid.Empty, new ChatRoom
            (Guid.Empty, new List<string> {"Dima", "Misha", "Olya", "Koka", "Kesha"}));
    }
}