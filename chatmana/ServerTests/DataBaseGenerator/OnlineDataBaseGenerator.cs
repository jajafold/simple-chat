using Infrastructure;
using Infrastructure.Services;
using ChatRoom = Infrastructure.ChatRoom;

namespace ServerTests;

public static class OnlineDataBaseGenerator
{
    public static readonly IServerDataBase DataBase;

    static OnlineDataBaseGenerator()
    {
        DataBase = new ServerDataBase();
        DataBase.Chatrooms.Add(DataBase.MainChat, new ChatRoom
            (DataBase.MainChat, new List<string> {"Gena", "Vasya", "Petya"}));
        DataBase.Chatrooms.Add(Guid.Empty, new ChatRoom
            (DataBase.MainChat, new List<string> {"Dima", "Misha", "Olya"}));
    }
}