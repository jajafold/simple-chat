using System;
using System.Collections.Generic;

namespace Infrastructure;

public class ChatRoom
{
    public readonly Guid Id;
    public readonly List<string>? Users;

    public ChatRoom(Guid id, List<string>? users = default ) 
    {
        Id = id;
        Users = users ?? new List<string>();
    }
}