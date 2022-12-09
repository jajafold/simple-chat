using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure;

[Table("ChatRooms")]
public class ChatRoom
{
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
        Users = new List<User>();
    }

    public string Name { get; set; }
    public string Admin { get; set; }
    public int MaxUsers { get; set; }
    public string? Password { get; set; }

    [Key] public Guid Id { get; set; }
    public List<User> Users { get; set; }
    public int UsersCount => Users.Count;
}