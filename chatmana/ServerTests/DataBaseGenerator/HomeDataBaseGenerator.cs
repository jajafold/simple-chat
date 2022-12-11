using Infrastructure;
using Infrastructure.Services;

namespace ServerTests;

public class HomeDataBaseGenerator : IDataBaseGenerator
{
    private IDataBase DataBase { get; }

    public HomeDataBaseGenerator(IDataBase dataBase)
    {
        DataBase = dataBase;
        ((ChatDbContext) dataBase).Database.EnsureCreated();
    }

    public IDataBase ConfigureDataBase()
    {
        DataBase.ChatRooms.Add(new ChatRoom("1", "1"));
        return DataBase;
    }
}