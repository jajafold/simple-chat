using System;
using System.Collections.Generic;
using Infrastructure.Messages;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public interface IDataBase
{
    public DbSet<ChatRoom> ChatRooms { get; }
    public DbSet<Message> Messages{ get; }
    public Guid AddRoom(string creator, string name, string? password, int capacity);
    public void Join(Guid chatroom, string login);
    public void Leave(Guid chatRoomId, string login);
    public void PostMessage<T>(T message) where T : Message;
}