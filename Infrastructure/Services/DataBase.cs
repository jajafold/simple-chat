using System;
using System.Collections.Generic;
using Infrastructure.Messages;

namespace Infrastructure.Services;

public class DataBase : IServerDataBase
{
    public Dictionary<Guid, ChatRoom> ChatRooms { get; }
    public List<Message> Messages { get; }

    public DataBase()
    {
        ChatRooms = new Dictionary<Guid, ChatRoom>();
        Messages = new List<Message>();
    }

    public Guid AddRoom(string creator, string name, string? password, int capacity)
    {
        var room = new ChatRoom(creator, name, password, capacity);
        ChatRooms.Add(room.Id, room);

        return room.Id;
    }

    public void Join(Guid chatRoomId, string login)
    {
        if (ChatRooms[chatRoomId].Users.Contains(login))
            throw new InvalidOperationException($"Login {login} is already in use");
        ChatRooms[chatRoomId].Users.Add(login);
    }

    public void Leave(Guid chatRoomId, string login)
    {
        if (!ChatRooms[chatRoomId].Users.Contains(login))
            throw new InvalidOperationException($"There's no {login} in room {chatRoomId.ToString()}");
        ChatRooms[chatRoomId].Users.Remove(login);
    }
    
    public void PostMessage<T>(T message) where T : Message
    {
        if (!ChatRooms[message.ChatRoom].Users.Contains(message.Name))
            throw new InvalidOperationException($"Login {message.Name} is not in this room");
        Messages.Add(message);
    }
}