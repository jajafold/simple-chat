using System;
using System.Collections.Generic;

namespace Infrastructure;

public class ChatRoom
{
    public readonly string Name;
    public readonly string Admin;
    public readonly int MaxUsers;
    
    public bool RequiresPassword => Password is not null;
    public readonly string? Password;
    
    public readonly Guid Id;
    public readonly List<string> Users;
    public int UsersCount => Users.Count;

    public ChatRoom(
        string admin, 
        string name, 
        string? password = null, 
        int maxUsers = 0)
    {
        if (password is not null) Password = password;
        if (maxUsers is not 0) MaxUsers = maxUsers;
        
        Id = Guid.NewGuid();
        Admin = admin;
        Name = name;
        Users = new List<string>();
    }
}