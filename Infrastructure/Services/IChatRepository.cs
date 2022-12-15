using System;
using System.Collections.Generic;
using Infrastructure.Messages;

namespace Infrastructure.Services;

public interface IChatRepository
{
    protected ChatDbContext ChatContext { get; }
    public Guid AddRoom(string creator, string name, string? password, int capacity);
    public void Join(Guid chatroom, string login);
    public void Leave(Guid chatRoomId, string login);
    public bool ContainsRoom(Guid chatRoomId);
    public void PostTextMessage<T>(T message) where T : TextMessage;
    public ChatRoom? GetRoomById(Guid id);
    public void Empty();
    public IEnumerable<ChatRoom> AllChatRooms { get; }
    public IEnumerable<Message> AllMessages { get; }
}