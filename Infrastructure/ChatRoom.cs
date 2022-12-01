using System;
using System.Collections.Generic;

namespace Infrastructure;

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