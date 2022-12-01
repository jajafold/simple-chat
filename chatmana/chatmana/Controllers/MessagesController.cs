using Infrastructure.Services;
using Infrastructure.Messages;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Infrastructure.Models;

namespace chatmana.Controllers;

public class MessagesController : Controller
{
    private IServerDataBase dataBase;
    public MessagesController(IServerDataBase dataBase)
    {
        this.dataBase = dataBase;
    }
    [HttpPost]
    public string Text(string message, string name, Guid chatRoom)
    {
        var msg = new TextMessage(message, chatRoom, DateTime.Now, name);
        dataBase.PostMessage(msg);
        return $"{msg.ToFlatString()}";
    }

    [HttpGet]
    public JsonResult ChatRoomMessages(long timestamp, Guid chatRoomId)
    {
        var settings = new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.All};
        var result = JsonConvert.SerializeObject(dataBase.Messages
            .Where(x => x.ChatRoom == chatRoomId)
            .Where(x => x.TimeStamp > timestamp)
            .ToMessagesViewModel(), Formatting.Indented, settings);
        return new JsonResult(result);
    }   
}