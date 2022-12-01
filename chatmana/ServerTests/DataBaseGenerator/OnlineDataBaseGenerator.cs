using Infrastructure;
using Infrastructure.Services;

namespace ServerTests;

public static class OnlineDataBaseGenerator
{
    public static readonly IServerDataBase DataBase;

    static OnlineDataBaseGenerator()
    {
        DataBase = new ServerDataBase();
        DataBase.Chatrooms.Add(DataBase.MainChat, new ChatRoom
            (DataBase.MainChat, new List<string> {"Gena", "Vasya", "Petya"}));
    }
}