#pragma warning disable CA1416

using Infrastructure.Services;
using Infrastructure.Messages;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class MessagesController : Controller
{
    private readonly ISerializer serializer;
    private readonly IServerDataBase dataBase;

    public MessagesController(IServerDataBase dataBase, ISerializer serializer)
    {
        this.dataBase = dataBase;
        this.serializer = serializer;
    }

    [HttpPost]
    public void Text(string message, string name, Guid chatRoomId)
    {
        DataBase.PostMessage(new TextMessage(message, chatRoomId, DateTime.Now, name));
    }
    
    //TODO : self-made exceptions
    [HttpGet]
    public JsonResult ChatRoomMessages(long timestamp, Guid chatRoomId)
    {
        if (!dataBase.Chatrooms.ContainsKey(chatRoomId))
            throw new InvalidOperationException($"chat {chatRoomId} is not exist");
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var result = serializer.Serialize(dataBase.Messages
            .Where(x => x.ChatRoom == chatRoomId)
            .Where(x => x.TimeStamp > timestamp)
            .ToMessagesViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }
}