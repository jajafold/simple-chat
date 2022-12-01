using Infrastructure;
using Infrastructure.Services;

namespace ServerTests;

public static class HomeDataBaseGenerator
{
    public static readonly IServerDataBase DataBase;

    static HomeDataBaseGenerator()
    {
        DataBase = new ServerDataBase();
        DataBase.Chatrooms.Add(DataBase.MainChat, new ChatRoom
            (DataBase.MainChat, new List<string> {"Gena", "Vasya", "Petya"}));
    }
}