using System;
using System.Collections.Generic;
using Infrastructure.Messages;

namespace Infrastructure.Services;

public static class DataBase
{
    public static readonly Dictionary<Guid, ChatRoom> Chatrooms = new();
    public static readonly List<string> Users = new();
    public static readonly List<Message> Messages = new();
    public static readonly Guid MainChat = Guid.NewGuid();

    static DataBase()
    {
        Chatrooms[MainChat] = new ChatRoom(MainChat);
    }

    public static void Join(Guid chatroom, string login)
    {
        //Stubbed with MainChat
        if (Chatrooms[MainChat].Users.Contains(login))
            throw new InvalidOperationException($"Login {login} is already in use");
        Chatrooms[MainChat].Users.Add(login);
    }

    public static void Leave(Guid chatRoomId, string login)
    {
        if (!Chatrooms[MainChat].Users.Contains(login))
            throw new InvalidOperationException($"There's no {login} in room {chatRoomId.ToString()}");
        Chatrooms[MainChat].Users.Remove(login);
    }
    
    public static void PostMessage<T>(T message) where T : Message
    {
        if (!Chatrooms[message.ChatRoom].Users.Contains(message.Name))
            throw new InvalidOperationException($"Login {message.Name} is not in this room");
        Messages.Add(message);
    }
}