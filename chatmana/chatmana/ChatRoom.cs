namespace chatmana;

public class ChatRoom
{
    public readonly Guid Id;
    public readonly List<string> Users;

    public ChatRoom(Guid id)
    {
        Id = id;
        Users = new List<string>();
    }
}