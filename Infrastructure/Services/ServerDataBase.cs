using System;
using System.Collections.Generic;
using Infrastructure.Messages;

namespace Infrastructure.Services;

public class ServerDataBase : IServerDataBase
{
    public Dictionary<Guid, ChatRoom> Chatrooms { get; } = new();
    public List<string> Users { get; } = new();
    public List<Message> Messages{ get; } = new();
    public Guid MainChat { get; set; } = Guid.NewGuid();

    public ServerDataBase()
    {
        Chatrooms.TryAdd(MainChat, new ChatRoom(MainChat, new List<string>()));
    }

    public void Join(Guid chatroom, string login)
    {
        //Stubbed with MainChat
        if (Chatrooms[chatroom].Users.Contains(login))
            throw new InvalidOperationException($"Login {login} is already in use");
        Chatrooms[chatroom].Users.Add(login);
    }

    public void Leave(Guid chatRoomId, string login)
    {
        if (!Chatrooms[chatRoomId].Users.Contains(login))
            throw new InvalidOperationException($"There's no {login} in room {chatRoomId.ToString()}");
        Chatrooms[chatRoomId].Users.Remove(login);
    }
    
    public void PostMessage<T>(T message) where T : Message
    {
        if (!Chatrooms[message.ChatRoom].Users.Contains(message.Name))
            throw new InvalidOperationException($"Login {message.Name} is not in this room");
        Messages.Add(message);
    }
}