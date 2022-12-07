using System;
using System.Collections.Generic;

namespace Infrastructure;

public class ChatRoom
{
    public readonly string Name;
    public readonly string Admin;
    public readonly int MaxUsers;
    
    public readonly bool RequiresPassword = false;
    public readonly string? Password = null;
    
    public readonly Guid Id;
    public readonly List<string> Users;
    public int UsersCount => Users.Count;

    public ChatRoom(string admin, string name)
    {
        Id = Guid.NewGuid();
        Admin = admin;
        Name = name;
        Users = new List<string>();
    }
}