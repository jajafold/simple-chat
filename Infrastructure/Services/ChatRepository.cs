using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Messages;

namespace Infrastructure.Services;

public class ChatRepository : IChatRepository
{
    public ChatRepository(ChatDbContext dbContext)
    {
        ChatContext = dbContext;
    }

    public IEnumerable<ChatRoom> AllChatRooms => ChatContext.ChatRooms.ToArray();
    public IEnumerable<Message> AllMessages => ChatContext.TextMessages.ToArray();
    public IEnumerable<Message> GetMessagesWith(Guid chatRoomId, long timestamp)
    {
        return AllMessages.Where(x => x.ChatRoom == chatRoomId)
            .Where(x => x.TimeStamp > timestamp);
    }

    public ChatDbContext ChatContext { get; }

    public Guid AddRoom(string creator, string name, string? password, int capacity)
    {
        var id = Guid.NewGuid();
        ChatContext.ChatRooms.Add(new ChatRoom(creator, name, id, password, capacity));
        ChatContext.SaveChanges();
        return id;
    }

    public void Join(Guid chatroom, string login)
    {
        var room = GetRoomById(chatroom);
        if (room.Users.Count != 0 && room.Users.Any(x => x == login))
            throw new InvalidOperationException($"Login {login} is already in use");
        room.Users.Add(login);
        ChatContext.ChatRooms.Update(room);
        ChatContext.SaveChanges();
    }

    public void Leave(Guid chatRoomId, string login)
    {
        var room = GetRoomById(chatRoomId);
        if (room.Users.Count != 0 && room.Users.All(user => user != login))
            throw new InvalidOperationException($"There's no {login} in room {chatRoomId.ToString()}");
        room.Users.Remove(login);
        ChatContext.ChatRooms.Update(room);
        ChatContext.SaveChanges();
    }

    public bool ContainsRoom(Guid chatRoomId)
    {
        return ChatContext.ChatRooms.Any(x => x.Id == chatRoomId);
    }

    public void PostTextMessage<T>(T message) where T : TextMessage
    {
        //stub with TextMessage
        if (GetRoomById(message.ChatRoom).Users.All(user => user != message.Name))
            throw new InvalidOperationException($"Login {message.Name} is not in this room");
        ChatContext.TextMessages.Add(message);
        ChatContext.SaveChanges();
    }

    //TODO: self-made exception for null result
    public ChatRoom GetRoomById(Guid id)
    {
        return ChatContext.ChatRooms.First(x => x.Id == id);
    }

    public void Empty()
    {
        ChatContext.Database.EnsureDeleted();
        ChatContext.Database.EnsureCreated();
    }
}