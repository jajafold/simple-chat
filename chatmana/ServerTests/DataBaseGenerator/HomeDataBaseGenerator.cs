using Infrastructure;
using Infrastructure.Services;

namespace ServerTests;

public class HomeDataBaseGenerator
{
    public readonly IServerDataBase DataBase;

    public HomeDataBaseGenerator()
    {
        DataBase = new ServerDataBase();
        DataBase.Chatrooms[DataBase.MainChat].Users!.AddRange(new List<string> {"Gena", "Vasya", "Petya"});
    }
}