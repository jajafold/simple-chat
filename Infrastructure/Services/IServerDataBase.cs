using System;
using System.Collections.Generic;
using Infrastructure.Messages;

namespace Infrastructure.Services;

public interface IServerDataBase
{
    public Dictionary<Guid, ChatRoom> Chatrooms { get; }
    public List<string> Users{ get; }
    public List<Message> Messages{ get; }
    public Guid MainChat { get; set; }
    public void Join(Guid chatroom, string login);
    public void Leave(Guid chatRoomId, string login);
    public void PostMessage<T>(T message) where T : Message;
}