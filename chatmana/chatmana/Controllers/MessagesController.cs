#pragma warning disable CA1416

using Infrastructure;
using Infrastructure.Messages;
using Infrastructure.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace chatmana.Controllers;

public class MessagesController : Controller
{
    private readonly IDataBase _dataBase;
    private readonly ISerializer _serializer;

    public MessagesController(IDataBase dataBase, ISerializer serializer)
    {
        _dataBase = dataBase;
        _serializer = serializer;
    }

    [HttpPost]
    public void Text(string message, string name, Guid chatRoomId)
    {
        _dataBase.PostMessage<TextMessage>(new TextMessage(message, chatRoomId, DateTime.Now, name));
    }

    //TODO : self-made exceptions
    [HttpGet]
    public JsonResult ChatRoomMessages(long timestamp, Guid chatRoomId)
    {
        if (!_dataBase.ChatRooms.Any(x => x.Id == chatRoomId))
            throw new InvalidOperationException($"chat {chatRoomId} is not exist");

        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var result = _serializer.Serialize(_dataBase.Messages
            .Where(x => x.ChatRoom == chatRoomId)
            .Where(x => x.TimeStamp > timestamp)
            .ToMessagesViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }
}