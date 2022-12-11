using Infrastructure.Services;

namespace ServerTests;

public interface IRepositoryGenerator
{
    public IChatRepository ConfigureRepository();
}