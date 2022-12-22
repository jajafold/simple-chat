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
        Guid id,
        string? password = null,
        int maxUsers = 0)
    {
        if (password is not null) Password = password;
        if (maxUsers is not 0) MaxUsers = maxUsers;

        Id = id;
        Admin = admin;
        Name = name;
        Users = new List<string>();
    }

    public string Name { get; set; }
    public string Admin { get; set; }
    public int MaxUsers { get; set; }
    public string? Password { get; set; }

    [Key] public Guid Id { get; set; }
    public List<string> Users { get; set; }
    public int UsersCount => Users.Count;
    public bool CanFit =>  MaxUsers == 0 || UsersCount < MaxUsers;
}