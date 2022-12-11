using Infrastructure.Services;

namespace ServerTests;

public class RepositoryGenerator : IRepositoryGenerator
{
    private IChatRepository ChatRepository { get; }

    public RepositoryGenerator(IChatRepository repository)
    {
        ChatRepository = repository;
        ChatRepository.Empty();
    }
    public IChatRepository ConfigureRepository()
    {
        var roomOne = ChatRepository.AddRoom("0", "0", "", 0);
        var roomTwo = ChatRepository.AddRoom("1", "1", "", 0);
        var usersRoomOne = new List<string> {"Gena", "Vasya", "Petya"};
        var usersRoomTwo = new List<string> {"Dima", "Misha", "Olya", "Koka", "Kesha"};
        foreach (var user in usersRoomOne)
            ChatRepository.Join(roomOne, user);
        foreach (var user in usersRoomTwo)
            ChatRepository.Join(roomTwo, user);
        return ChatRepository;
    }
}