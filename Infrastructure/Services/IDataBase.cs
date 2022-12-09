using System;
using Infrastructure.Messages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public interface IDataBase
{
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<TextMessage> Messages { get; set; }
    public Guid AddRoom(string creator, string name, string? password, int capacity);
    public void Join(Guid chatroom, string login);
    public void Leave(Guid chatRoomId, string login);
    public void PostMessage<T>(TextMessage message) where T : Message;
    public ChatRoom? GetRoomById(Guid id);
}