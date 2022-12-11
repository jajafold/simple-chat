using Infrastructure.Services;

namespace ServerTests;

public interface IDataBaseGenerator
{
    public IDataBase ConfigureDataBase();
}