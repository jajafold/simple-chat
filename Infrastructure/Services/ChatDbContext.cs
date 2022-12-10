using System;
using System.Linq;
using Infrastructure.Messages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ChatDbContext : DbContext, IDataBase
{
    public ChatDbContext(DbContextOptions<ChatDbContext>
        options) : base(options)
    {
    }

    public DbSet<ChatRoom> ChatRooms { get; set; }

    public DbSet<TextMessage> Messages { get; set; }

    public Guid AddRoom(string creator, string name, string? password, int capacity)
    {
        var id = Guid.NewGuid();
        ChatRooms.Add(new ChatRoom(creator, name, password, capacity));
        return id;
    }

    public void Join(Guid chatroom, string login)
    {
        if (GetRoomById(chatroom).Users.Any(x => x.Name == login))
            throw new InvalidOperationException($"Login {login} is already in use");
        GetRoomById(chatroom).Users.Add(new User {Name = login});
    }

    public void Leave(Guid chatRoomId, string login)
    {
        if (!GetRoomById(chatRoomId).Users.Any(user => user.Name == login))
            throw new InvalidOperationException($"There's no {login} in room {chatRoomId.ToString()}");
        GetRoomById(chatRoomId).Users.Remove(new User {Name = login});
    }

    public void PostMessage<T>(TextMessage message) where T : Message
    {
        //stub with TextMessage
        if (!GetRoomById(message.ChatRoom).Users.Any(user => user.Name == message.Name))
            throw new InvalidOperationException($"Login {message.Name} is not in this room");
        Messages.Add(message);
    }

    //TODO: self-made exception for null result
    public ChatRoom? GetRoomById(Guid id)
    {
        return ChatRooms.FirstOrDefault(x => x.Id == id);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TextMessage>();

        base.OnModelCreating(builder);
    }
}